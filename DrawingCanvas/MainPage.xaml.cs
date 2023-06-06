namespace DrawingCanvas;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class MainPage : ContentPage
{
    GraphicsView m_objDrawCanvas = null;
    bool m_StartInteraction = false;
    bool m_StartInteractionWithAnchor = false;
    int m_nSelObjIdx = -1;
    float m_fOffsetX = 0, m_fOffsetY = 0;

    public MainPage()
	{
		InitializeComponent();

        m_objDrawCanvas = (GraphicsView)FindByName("DrawCanvas");

        Init();
    }

    void Init(ObjectType p_enType = ObjectType.None)
    {this.InvalidateMeasure();
        if (p_enType == ObjectType.None)
        {
            (FindByName("lbl_width") as Label).IsEnabled = false;
            (FindByName("editor_width") as Entry).IsEnabled = false;
            (FindByName("lbl_height") as Label).IsEnabled = false;
            (FindByName("editor_height") as Entry).IsEnabled = false;
            (FindByName("lbl_radius") as Label).IsEnabled = false;
            (FindByName("editor_radius") as Entry).IsEnabled = false;

            (FindByName("lbl_text") as Label).IsEnabled = false;
            (FindByName("editor_text") as Editor).IsEnabled = false;
            (FindByName("btn_apply") as Button).IsEnabled = false; 

            (FindByName("lbl_bordercolor") as Label).IsEnabled = false;
            (FindByName("picker_border") as Picker).IsEnabled = false;
            (FindByName("lbl_backgroundcolor") as Label).IsEnabled = false;
            (FindByName("picker_background") as Picker).IsEnabled = false;
        }
        else if(p_enType == ObjectType.Rectangle) // Rectangle
        {
            (FindByName("lbl_width") as Label).IsEnabled = true;
            (FindByName("editor_width") as Entry).IsEnabled = true;
            (FindByName("lbl_height") as Label).IsEnabled = true;
            (FindByName("editor_height") as Entry).IsEnabled = true;

            (FindByName("lbl_text") as Label).IsEnabled = false;
            (FindByName("editor_text") as Editor).IsEnabled = false;
            (FindByName("btn_apply") as Button).IsEnabled = false;

            (FindByName("lbl_bordercolor") as Label).IsEnabled = true;
            (FindByName("picker_border") as Picker).IsEnabled = true;
            (FindByName("lbl_backgroundcolor") as Label).IsEnabled = true;
            (FindByName("picker_background") as Picker).IsEnabled = true;


            (FindByName("editor_width") as Entry).Text = (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as RectangelObject).m_fWidth.ToString();
            (FindByName("editor_height") as Entry).Text = (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as RectangelObject).m_fHeight.ToString();
        }
        else if(p_enType == ObjectType.Circle) // Circle
        {
            (FindByName("lbl_radius") as Label).IsEnabled = true;
            (FindByName("editor_radius") as Entry).IsEnabled = true;

            (FindByName("lbl_text") as Label).IsEnabled = false;
            (FindByName("editor_text") as Editor).IsEnabled = false;
            (FindByName("btn_apply") as Button).IsEnabled = false;

            (FindByName("lbl_bordercolor") as Label).IsEnabled = true;
            (FindByName("picker_border") as Picker).IsEnabled = true;
            (FindByName("lbl_backgroundcolor") as Label).IsEnabled = true;
            (FindByName("picker_background") as Picker).IsEnabled = true;


            (FindByName("editor_radius") as Entry).Text = (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as CircleObject).m_fRadius.ToString();
        }
        else if(p_enType == ObjectType.Bubble) // Speech Bubble
        {
            //(FindByName("lbl_width") as Label).IsVisible = true;
            //(FindByName("editor_width") as Entry).IsVisible = true;
            //(FindByName("lbl_height") as Label).IsVisible = true;
            //(FindByName("editor_height") as Entry).IsVisible = true;

            (FindByName("lbl_text") as Label).IsEnabled = true;
            (FindByName("editor_text") as Editor).IsEnabled = true;
            (FindByName("btn_apply") as Button).IsEnabled = true;

            (FindByName("lbl_bordercolor") as Label).IsEnabled = true;
            (FindByName("picker_border") as Picker).IsEnabled = true;
            (FindByName("lbl_backgroundcolor") as Label).IsEnabled = true;
            (FindByName("picker_background") as Picker).IsEnabled = true;


            //(FindByName("editor_width") as Entry).Text = (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as SpeechBubbleObject).m_fWidth.ToString();
            //(FindByName("editor_height") as Entry).Text = (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as SpeechBubbleObject).m_fHeight.ToString();
            (FindByName("editor_text") as Editor).Text = (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as SpeechBubbleObject).m_strSpeech;
        }
    }

    void OnBtnAddRectangleClicked(object sender, EventArgs e)
    {
        DrawObjects.AddRectangle(10, 10, 20, 20);

        //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
        m_objDrawCanvas.Invalidate();

        m_nSelObjIdx = -1;
        DrawObjects.m_nSelObjIdx = -1;

        Init(ObjectType.None);
    }

    void OnBtnAddCircleClicked(object sender, EventArgs e)
    {
        DrawObjects.AddCircle(30, 30, 20);

        //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
        m_objDrawCanvas.Invalidate();

        m_nSelObjIdx = -1;
        DrawObjects.m_nSelObjIdx = -1;

        Init(ObjectType.None);
    }

    void OnBtnAddBubbleClicked(object sender, EventArgs e)
    {
        DrawObjects.AddSpeechBubble(10, 10, 200, 100);

        //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
        m_objDrawCanvas.Invalidate();

        m_nSelObjIdx = -1;
        DrawObjects.m_nSelObjIdx = -1;

        Init(ObjectType.None);
    }

    private void OnBtnApplyClicked(object sender, EventArgs e)
    {
        if (m_nSelObjIdx == -1)
            return;

        string w_strValue = (FindByName("editor_text") as Editor).Text;

        (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as SpeechBubbleObject).m_strSpeech = w_strValue;

        //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
        m_objDrawCanvas.Invalidate();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        Frame CanvasRegionBorder = (Frame)FindByName("CanvasRegionBorder");
        StackLayout w_objCanvasRegion = (StackLayout)FindByName("CanvasRegion");
        m_objDrawCanvas.WidthRequest = w_objCanvasRegion.Width;// - (CanvasRegionBorder.Margin.Left + CanvasRegionBorder.Margin.Right);
        m_objDrawCanvas.HeightRequest = w_objCanvasRegion.Height;// - (CanvasRegionBorder.Margin.Top + CanvasRegionBorder.Margin.Bottom);
    }

    private void ContentPage_SizeChanged(object sender, EventArgs e)
    {
        Frame CanvasRegionBorder = (Frame)FindByName("CanvasRegionBorder");
        StackLayout w_objCanvasRegion = (StackLayout)FindByName("CanvasRegion");
        m_objDrawCanvas.WidthRequest = w_objCanvasRegion.Width;// - (CanvasRegionBorder.Margin.Left + CanvasRegionBorder.Margin.Right);
        m_objDrawCanvas.HeightRequest = w_objCanvasRegion.Height;// - (CanvasRegionBorder.Margin.Top + CanvasRegionBorder.Margin.Bottom);
    }

    private void picker_border_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (m_nSelObjIdx == -1)
            return;

        string w_strColorName = (FindByName("picker_border") as Picker).SelectedItem as string;
        Color w_enColor = Colors.Black;

        if(w_strColorName.Equals("Red"))
            w_enColor = Colors.Red;
        else if (w_strColorName.Equals("Yellow"))
            w_enColor = Colors.Yellow;
        else if (w_strColorName.Equals("Blue"))
            w_enColor = Colors.Blue;
        else if (w_strColorName.Equals("Green"))
            w_enColor = Colors.Green;
        else if (w_strColorName.Equals("Orange"))
            w_enColor = Colors.Orange;
        else if (w_strColorName.Equals("Purple"))
            w_enColor = Colors.Purple;
        else if (w_strColorName.Equals("Black"))
            w_enColor = Colors.Black;
        else if (w_strColorName.Equals("White"))
            w_enColor = Colors.White;


        if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Rectangle)
        {
            RectangelObject w_clsRectangleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as RectangelObject;
            w_clsRectangleObj.m_enBorderColor = w_enColor;
        }
        if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Circle)
        {
            CircleObject w_clsRectangleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as CircleObject;
            w_clsRectangleObj.m_enBorderColor = w_enColor;
        }
        else if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Bubble)
        {
            SpeechBubbleObject w_clsSpeechBubbleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as SpeechBubbleObject;
            w_clsSpeechBubbleObj.m_enBorderColor = w_enColor;
        }

        //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
        m_objDrawCanvas.Invalidate();
    }


    private void picker_background_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (m_nSelObjIdx == -1)
            return;

        string w_strColorName = (FindByName("picker_background") as Picker).SelectedItem as string;
        Color w_enColor = Colors.White;

        if (w_strColorName.Equals("Red"))
            w_enColor = Colors.Red;
        else if (w_strColorName.Equals("Yellow"))
            w_enColor = Colors.Yellow;
        else if (w_strColorName.Equals("Blue"))
            w_enColor = Colors.Blue;
        else if (w_strColorName.Equals("Green"))
            w_enColor = Colors.Green;
        else if (w_strColorName.Equals("Orange"))
            w_enColor = Colors.Orange;
        else if (w_strColorName.Equals("Purple"))
            w_enColor = Colors.Purple;
        else if (w_strColorName.Equals("Black"))
            w_enColor = Colors.Black;
        else if (w_strColorName.Equals("White"))
            w_enColor = Colors.White;


        if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Rectangle)
        {
            RectangelObject w_clsRectangleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as RectangelObject;
            w_clsRectangleObj.m_enBackColor = w_enColor;
        }
        if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Circle)
        {
            CircleObject w_clsRectangleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as CircleObject;
            w_clsRectangleObj.m_enBackColor = w_enColor;
        }
        else if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Bubble)
        {
            SpeechBubbleObject w_clsSpeechBubbleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as SpeechBubbleObject;
            w_clsSpeechBubbleObj.m_enBackColor = w_enColor;
        }

        //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
        m_objDrawCanvas.Invalidate();
    }

    private void OnTextCompleted(object sender, EventArgs e)
    {
        string w_strStyleId = (sender as Entry).StyleId;
        string w_strValue = "";

        if (m_nSelObjIdx == -1)
            return;

        if (w_strStyleId.Equals("editor_width"))
        {
            w_strValue = (FindByName("editor_width") as Entry).Text;
            if (w_strValue.Equals(""))
            {
                w_strValue = "20";
                (FindByName("editor_width") as Entry).Text = w_strValue;
            }

            if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Rectangle)
            {
                RectangelObject w_clsRectangleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as RectangelObject;
                w_clsRectangleObj.m_fWidth = Convert.ToSingle(w_strValue);
            }
            else if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Bubble)
            {
                SpeechBubbleObject w_clsSpeechBubbleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as SpeechBubbleObject;

                if (Convert.ToSingle(w_strValue) <= w_clsSpeechBubbleObj.m_fCornerRadius * 2)
                {
                    w_clsSpeechBubbleObj.m_fWidth = w_clsSpeechBubbleObj.m_fCornerRadius * 2;
                    (FindByName("editor_width") as Entry).Text = (w_clsSpeechBubbleObj.m_fCornerRadius * 2).ToString();
                }
                else
                    w_clsSpeechBubbleObj.m_fWidth = Convert.ToSingle(w_strValue);
            }
        }
        else if (w_strStyleId.Equals("editor_height"))
        {
            w_strValue = (FindByName("editor_height") as Entry).Text;
            if (w_strValue.Equals(""))
            {
                w_strValue = "20";
                (FindByName("editor_height") as Entry).Text = w_strValue;
            }

            if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Rectangle)
            {
                RectangelObject w_clsRectangleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as RectangelObject;
                w_clsRectangleObj.m_fHeight = Convert.ToSingle(w_strValue);
            }
            else if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Bubble)
            {
                SpeechBubbleObject w_clsSpeechBubbleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as SpeechBubbleObject;

                if (Convert.ToSingle(w_strValue) <= w_clsSpeechBubbleObj.m_fCornerRadius * 2)
                {
                    w_clsSpeechBubbleObj.m_fHeight = w_clsSpeechBubbleObj.m_fCornerRadius * 2;
                    (FindByName("editor_height") as Entry).Text = (w_clsSpeechBubbleObj.m_fCornerRadius * 2).ToString();
                }
                else
                    w_clsSpeechBubbleObj.m_fHeight = Convert.ToSingle(w_strValue);
            }
        }
        else if (w_strStyleId.Equals("editor_radius"))
        {
            w_strValue = (FindByName("editor_radius") as Entry).Text;
            if (w_strValue.Equals(""))
            {
                w_strValue = "20";
                (FindByName("editor_radius") as Entry).Text = w_strValue;
            }

            if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Circle)
            {
                CircleObject w_clsRectangleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as CircleObject;
                w_clsRectangleObj.m_fRadius = Convert.ToSingle(w_strValue);
            }
        }

        //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
        m_objDrawCanvas.Invalidate();
    }

    private void OnTextCompleted1(object sender, EventArgs e)
    {
        /*if (m_nSelObjIdx == -1)
            return;

        string w_strValue = (FindByName("editor_text") as Editor).Text;

        (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as SpeechBubbleObject).m_strSpeech = w_strValue;

        //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
        m_objDrawCanvas.Invalidate();*/
    }

    private void OnStartInteraction(object Sender, TouchEventArgs evt)
    {
        PointF w_ptStart;

        m_StartInteraction = true;
        m_nSelObjIdx = -1;
        DrawObjects.m_nSelObjIdx = -1;

        w_ptStart = evt.Touches.FirstOrDefault();

        for (int i = DrawObjects.m_objDrawObjectList.Count - 1; i >= 0; i--)
        {
            if(DrawObjects.IsInObject(DrawObjects.m_objDrawObjectList[i], w_ptStart))
            {
                m_nSelObjIdx = i;
                DrawObjects.m_nSelObjIdx = i;

                Init(ObjectType.None);

                float w_fX = 0, w_fY = 0;
                Color w_enBorderColor = Colors.Black;
                Color w_enBackColor = Colors.White;

                if (DrawObjects.m_objDrawObjectList[i].m_enObjectType == ObjectType.Rectangle)
                {
                    w_fX = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as RectangelObject).m_fPosX;
                    w_fY = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as RectangelObject).m_fPosY;
                    w_enBorderColor = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as RectangelObject).m_enBorderColor;
                    w_enBackColor = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as RectangelObject).m_enBackColor;

                    Init(ObjectType.Rectangle);
                }
                else if (DrawObjects.m_objDrawObjectList[i].m_enObjectType == ObjectType.Circle)
                {
                    w_fX = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as CircleObject).m_fPosX;
                    w_fY = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as CircleObject).m_fPosY;
                    w_enBorderColor = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as CircleObject).m_enBorderColor;
                    w_enBackColor = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as CircleObject).m_enBackColor;

                    Init(ObjectType.Circle);
                }
                else if (DrawObjects.m_objDrawObjectList[i].m_enObjectType == ObjectType.Bubble)
                {
                    SpeechBubbleObject w_clsSpeechBubbleObj = DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as SpeechBubbleObject;
                    if (
                        DrawObjects.PointInTriangle
                        (
                            new PointF(w_ptStart.X, w_ptStart.Y),
                            new PointF(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset + w_clsSpeechBubbleObj.m_fSpeechWidth, w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight),
                            new PointF(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset + w_clsSpeechBubbleObj.m_fSpeechDelta, w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight + w_clsSpeechBubbleObj.m_fSpeechDepth),
                            new PointF(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset, w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight)
                        )
                      )
                        m_StartInteractionWithAnchor = true;

                    w_fX = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as SpeechBubbleObject).m_fPosX;
                    w_fY = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as SpeechBubbleObject).m_fPosY;
                    w_enBorderColor = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as SpeechBubbleObject).m_enBorderColor;
                    w_enBackColor = (DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as SpeechBubbleObject).m_enBackColor;

                    Init(ObjectType.Bubble);
                }

                m_fOffsetX = w_fX - w_ptStart.X;
                m_fOffsetY = w_fY - w_ptStart.Y;


                if (w_enBorderColor == Colors.Red)
                    (FindByName("picker_border") as Picker).SelectedItem = "Red";
                else if (w_enBorderColor == Colors.Yellow)
                    (FindByName("picker_border") as Picker).SelectedItem = "Yellow";
                else if (w_enBorderColor == Colors.Blue)
                    (FindByName("picker_border") as Picker).SelectedItem = "Blue";
                else if (w_enBorderColor == Colors.Green)
                    (FindByName("picker_border") as Picker).SelectedItem = "Green";
                else if (w_enBorderColor == Colors.Orange)
                    (FindByName("picker_border") as Picker).SelectedItem = "Orange";
                else if (w_enBorderColor == Colors.Purple)
                    (FindByName("picker_border") as Picker).SelectedItem = "Purple";
                else if (w_enBorderColor == Colors.Black)
                    (FindByName("picker_border") as Picker).SelectedItem = "Black";
                else if (w_enBorderColor == Colors.White)
                    (FindByName("picker_border") as Picker).SelectedItem = "White";

                if (w_enBackColor == Colors.Red)
                    (FindByName("picker_background") as Picker).SelectedItem = "Red";
                else if (w_enBackColor == Colors.Yellow)
                    (FindByName("picker_background") as Picker).SelectedItem = "Yellow";
                else if (w_enBackColor == Colors.Blue)
                    (FindByName("picker_background") as Picker).SelectedItem = "Blue";
                else if (w_enBackColor == Colors.Green)
                    (FindByName("picker_background") as Picker).SelectedItem = "Green";
                else if (w_enBackColor == Colors.Orange)
                    (FindByName("picker_background") as Picker).SelectedItem = "Orange";
                else if (w_enBackColor == Colors.Purple)
                    (FindByName("picker_background") as Picker).SelectedItem = "Purple";
                else if (w_enBackColor == Colors.Black)
                    (FindByName("picker_background") as Picker).SelectedItem = "Black";
                else if (w_enBackColor == Colors.White)
                    (FindByName("picker_background") as Picker).SelectedItem = "White";

                //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
                m_objDrawCanvas.Invalidate();

                break;
            }
        }

        if(m_nSelObjIdx == -1)
        {
            Init(ObjectType.None);

            //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
            m_objDrawCanvas.Invalidate();
        }
    }
    private void OnEndInteraction(object Sender, TouchEventArgs evt)
    {
        m_StartInteraction = false;
        m_StartInteractionWithAnchor = false;
        //m_nSelObjIdx = -1;
    }


    private void OnHoverInteraction(object sender, TouchEventArgs evt)
    {
        return;

        PointF w_ptHoverStart;

        w_ptHoverStart = evt.Touches.FirstOrDefault();

        if(m_StartInteractionWithAnchor)
        {
            DrawObjects.MoveAnchor(DrawObjects.m_objDrawObjectList[m_nSelObjIdx], w_ptHoverStart);
        }
        else if (m_StartInteraction && m_nSelObjIdx != -1)
        {
            float w_fPosX = 0, w_fPosY = 0, w_fWidth = 0, w_fHeight = 0;
            if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Rectangle)
            {
                RectangelObject w_clsRectangleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as RectangelObject;

                w_fPosX = w_ptHoverStart.X + m_fOffsetX - 4;
                w_fPosY = w_ptHoverStart.Y + m_fOffsetY - 4;
                w_fWidth = w_clsRectangleObj.m_fWidth + 8;
                w_fHeight = w_clsRectangleObj.m_fHeight + 8;

            }
            else if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Circle)
            {
                CircleObject w_clsCircleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as CircleObject;

                w_fPosX = w_ptHoverStart.X + m_fOffsetX - w_clsCircleObj.m_fRadius - 4;
                w_fPosY = w_ptHoverStart.Y + m_fOffsetY - w_clsCircleObj.m_fRadius - 4;
                w_fWidth = w_clsCircleObj.m_fRadius * 2 + 8;
                w_fHeight = w_clsCircleObj.m_fRadius * 2 + 8;
            }
            else if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Bubble)
            {
                SpeechBubbleObject w_clsSpeechBubbleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as SpeechBubbleObject;

                w_fPosX = w_ptHoverStart.X + m_fOffsetX - 4;
                w_fPosY = w_ptHoverStart.Y + m_fOffsetY - 4;
                w_fWidth = w_clsSpeechBubbleObj.m_fWidth + 8;
                w_fHeight = w_clsSpeechBubbleObj.m_fHeight + w_clsSpeechBubbleObj.m_fSpeechDepth + 8;
            }

            if (w_fPosX <= 0 || w_fPosY <= 0)
            {

            }
            else if(w_fPosX + w_fWidth  > m_objDrawCanvas.Width - 4 || w_fPosY + w_fHeight > m_objDrawCanvas.Height - 4)
            {
            }
            else
                DrawObjects.MoveObject(DrawObjects.m_objDrawObjectList[m_nSelObjIdx], w_ptHoverStart, m_fOffsetX, m_fOffsetY);
        }

        //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
        m_objDrawCanvas.Invalidate();
    }

    void OnBtnSaveClicked(object sender, EventArgs e)
    {
        string fileName = Path.Combine(FileSystem.Current.AppDataDirectory, "temp.txt");//address

        string w_strContent = JsonConvert.SerializeObject(DrawObjects.m_objDrawObjectList);
        File.WriteAllText(fileName, w_strContent);
    }

    private void DrawCanvas_DragInteraction(object sender, TouchEventArgs evt)
    {
        PointF w_ptHoverStart;

        w_ptHoverStart = evt.Touches.FirstOrDefault();

        if (m_StartInteractionWithAnchor)
        {
            DrawObjects.MoveAnchor(DrawObjects.m_objDrawObjectList[m_nSelObjIdx], w_ptHoverStart);
        }
        else if (m_StartInteraction && m_nSelObjIdx != -1)
        {
            float w_fPosX = 0, w_fPosY = 0, w_fWidth = 0, w_fHeight = 0;
            if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Rectangle)
            {
                RectangelObject w_clsRectangleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as RectangelObject;

                w_fPosX = w_ptHoverStart.X + m_fOffsetX - 4;
                w_fPosY = w_ptHoverStart.Y + m_fOffsetY - 4;
                w_fWidth = w_clsRectangleObj.m_fWidth + 8;
                w_fHeight = w_clsRectangleObj.m_fHeight + 8;

            }
            else if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Circle)
            {
                CircleObject w_clsCircleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as CircleObject;

                w_fPosX = w_ptHoverStart.X + m_fOffsetX - w_clsCircleObj.m_fRadius - 4;
                w_fPosY = w_ptHoverStart.Y + m_fOffsetY - w_clsCircleObj.m_fRadius - 4;
                w_fWidth = w_clsCircleObj.m_fRadius * 2 + 8;
                w_fHeight = w_clsCircleObj.m_fRadius * 2 + 8;
            }
            else if (DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_enObjectType == ObjectType.Bubble)
            {
                SpeechBubbleObject w_clsSpeechBubbleObj = DrawObjects.m_objDrawObjectList[m_nSelObjIdx].m_clsDrawObject as SpeechBubbleObject;

                w_fPosX = w_ptHoverStart.X + m_fOffsetX - 4;
                w_fPosY = w_ptHoverStart.Y + m_fOffsetY - 4;
                w_fWidth = w_clsSpeechBubbleObj.m_fWidth + 8;
                w_fHeight = w_clsSpeechBubbleObj.m_fHeight + w_clsSpeechBubbleObj.m_fSpeechDepth + 8;
            }

            if (w_fPosX <= 0 || w_fPosY <= 0)
            {

            }
            else if (w_fPosX + w_fWidth > m_objDrawCanvas.Width - 4 || w_fPosY + w_fHeight > m_objDrawCanvas.Height - 4)
            {
            }
            else
                DrawObjects.MoveObject(DrawObjects.m_objDrawObjectList[m_nSelObjIdx], w_ptHoverStart, m_fOffsetX, m_fOffsetY);
        }

        //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
        m_objDrawCanvas.Invalidate();
    }

    void OnBtnLoadClicked(object sender, EventArgs e)
    {
        string fileName = Path.Combine(FileSystem.Current.AppDataDirectory, "temp.txt");//address

        string w_strContent = File.ReadAllText(fileName);
        var objects = JArray.Parse(w_strContent);

        int w_nIdx = 0;
        DrawObjects.m_objDrawObjectList.Clear();

        foreach (JObject root in objects)
        {
            int w_nType = (int)root["m_enObjectType"];
            JObject detail = (JObject)root["m_clsDrawObject"];

            string w_strBorderColor = (string)detail["m_enBorderColor"];
            //w_strBorderColor = w_strBorderColor.Replace("#", "0x");
            string w_strBackColor = (string)detail["m_enBackColor"];
            //w_strBackColor = w_strBackColor.Replace("#", "0x");

            if (w_nType == 1)
            {
                DrawObjects.AddRectangle((float)detail["m_fPosX"], (float)detail["m_fPosY"], (float)detail["m_fWidth"], (float)detail["m_fHeight"]);
                (DrawObjects.m_objDrawObjectList[w_nIdx].m_clsDrawObject as RectangelObject).m_enBorderColor = Color.FromArgb(w_strBorderColor);
                (DrawObjects.m_objDrawObjectList[w_nIdx].m_clsDrawObject as RectangelObject).m_enBackColor = Color.FromArgb(w_strBackColor);
            }
            else if(w_nType == 2)
            {
                DrawObjects.AddCircle((float)detail["m_fPosX"], (float)detail["m_fPosY"], (float)detail["m_fRadius"]);
                (DrawObjects.m_objDrawObjectList[w_nIdx].m_clsDrawObject as CircleObject).m_enBorderColor = Color.FromArgb(w_strBorderColor);
                (DrawObjects.m_objDrawObjectList[w_nIdx].m_clsDrawObject as CircleObject).m_enBackColor = Color.FromArgb(w_strBackColor);
            }
            else if(w_nType == 3)
            {
                DrawObjects.AddSpeechBubble((float)detail["m_fPosX"], (float)detail["m_fPosY"], (float)detail["m_fWidth"], (float)detail["m_fHeight"]);
                (DrawObjects.m_objDrawObjectList[w_nIdx].m_clsDrawObject as SpeechBubbleObject).m_strSpeech = (string)detail["m_strSpeech"];
                (DrawObjects.m_objDrawObjectList[w_nIdx].m_clsDrawObject as SpeechBubbleObject).m_fSpeechDelta = (float)detail["m_fSpeechDelta"];
                (DrawObjects.m_objDrawObjectList[w_nIdx].m_clsDrawObject as SpeechBubbleObject).m_fSpeechDepth = (float)detail["m_fSpeechDepth"];
                (DrawObjects.m_objDrawObjectList[w_nIdx].m_clsDrawObject as SpeechBubbleObject).m_enBorderColor = Color.FromArgb(w_strBorderColor);
                (DrawObjects.m_objDrawObjectList[w_nIdx].m_clsDrawObject as SpeechBubbleObject).m_enBackColor = Color.FromArgb(w_strBackColor);
            }

            w_nIdx++;
        }

        //m_objDrawCanvas.FlowDirection = FlowDirection.MatchParent;
        m_objDrawCanvas.Invalidate();

        m_nSelObjIdx = -1;
        DrawObjects.m_nSelObjIdx = -1;

        Init(ObjectType.None);
    }
}