using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DrawingCanvas
{
    enum ObjectType{None, Rectangle, Circle, Bubble};


    class RectangelObject
    {
        public float m_fPosX = 0;
        public float m_fPosY = 0;
        public float m_fWidth = 0;
        public float m_fHeight = 0;
        public Color m_enBorderColor = Colors.Black;
        public Color m_enBackColor = Colors.White;

        public RectangelObject(float p_fPosX, float p_fPosY, float p_fWidth, float p_fHeight, Color p_enBorderColor, Color p_enBackColor)
        {
            m_fPosX = p_fPosX;
            m_fPosY = p_fPosY;
            m_fWidth = p_fWidth;
            m_fHeight = p_fHeight;
            m_enBorderColor = p_enBorderColor;
            m_enBackColor = p_enBackColor;
        }
    }

    class CircleObject
    {
        public float m_fPosX = 0;
        public float m_fPosY = 0;
        public float m_fRadius = 0;
        public Color m_enBorderColor = Colors.Black;
        public Color m_enBackColor = Colors.White;

        public CircleObject(float p_fPosX, float p_fPosY, float p_fRadius, Color p_enBorderColor, Color p_enBackColor)
        {
            m_fPosX = p_fPosX;
            m_fPosY = p_fPosY;
            m_fRadius = p_fRadius;
            m_enBorderColor = p_enBorderColor;
            m_enBackColor = p_enBackColor;
        }
    }

    class SpeechBubbleObject
    {
        public float m_fPosX = 0;
        public float m_fPosY = 0;
        public float m_fWidth = 200;
        public float m_fHeight = 100;
        public Color m_enBorderColor = Colors.Black;
        public Color m_enBackColor = Colors.White;

        public float m_fCornerRadius = 10;
        public float m_fSpeechOffset = 30;
        public float m_fSpeechDepth = 20;
        public float m_fSpeechWidth = 20;

        public float m_fSpeechDelta = 0;

        public string m_strSpeech = "";

        public SpeechBubbleObject(float p_fPosX, float p_fPosY, float p_fWidth, float p_fHeight, string p_strSpeech, Color p_enBorderColor, Color p_enBackColor)
        {
            m_fPosX = p_fPosX;
            m_fPosY = p_fPosY;
            m_fWidth = p_fWidth;
            m_fHeight = p_fHeight;
            m_strSpeech = p_strSpeech;
            m_enBorderColor = p_enBorderColor;
            m_enBackColor = p_enBackColor;
        }
    }

    class DrawObject
    {
        public ObjectType m_enObjectType = ObjectType.Rectangle;
        public object m_clsDrawObject;

        public DrawObject(object p_clsDrawObje, ObjectType p_enObjectType)
        {
            m_clsDrawObject = p_clsDrawObje;
            m_enObjectType = p_enObjectType;
        }
    }

    static class DrawObjects
    {
        public static List<DrawObject> m_objDrawObjectList = new List<DrawObject>();
        public static int m_nSelObjIdx = -1;
        
        public static void AddRectangle(float p_fPosX, float p_fPosY, float p_fWidth, float p_fHeight)
        {
            RectangelObject w_clsRectangleObj = new RectangelObject(p_fPosX, p_fPosY, p_fWidth, p_fHeight, Colors.Black, Colors.White);
            DrawObject w_clsDrawObj = new DrawObject(w_clsRectangleObj, ObjectType.Rectangle);

            m_objDrawObjectList.Add(w_clsDrawObj);
        }

        public static void AddCircle(float p_fPosX, float p_fPosY, float p_fRadius)
        {
            CircleObject w_clsRectangleObj = new CircleObject(p_fPosX, p_fPosY, p_fRadius, Colors.Black, Colors.White);
            DrawObject w_clsDrawObj = new DrawObject(w_clsRectangleObj, ObjectType.Circle);

            m_objDrawObjectList.Add(w_clsDrawObj);
        }

        public static void AddSpeechBubble(float p_fPosX, float p_fPosY, float p_fWidth, float p_fHeight)
        {
            SpeechBubbleObject w_clsSpeechBubbleObj = new SpeechBubbleObject(p_fPosX, p_fPosY, p_fWidth, p_fHeight, "test bubble", Colors.Black, Colors.White);
            DrawObject w_clsDrawObj = new DrawObject(w_clsSpeechBubbleObj, ObjectType.Bubble);

            m_objDrawObjectList.Add(w_clsDrawObj);
        }

        public static bool IsInObject(DrawObject p_clsDrawObject, PointF p_fPoint)
        {
            bool w_bRet = false;

            if (p_clsDrawObject.m_enObjectType == ObjectType.Rectangle)
            {
                RectangelObject w_clsRectangleObject = p_clsDrawObject.m_clsDrawObject as RectangelObject;
                if (
                    (p_fPoint.X > w_clsRectangleObject.m_fPosX - 1 && p_fPoint.X < w_clsRectangleObject.m_fPosX + w_clsRectangleObject.m_fWidth + 1) &&
                    (p_fPoint.Y > w_clsRectangleObject.m_fPosY - 1 && p_fPoint.Y < w_clsRectangleObject.m_fPosY + w_clsRectangleObject.m_fHeight + 1)
                    )
                    w_bRet = true;
            }
            else if (p_clsDrawObject.m_enObjectType == ObjectType.Circle)
            {
                CircleObject w_clsCircleObject = p_clsDrawObject.m_clsDrawObject as CircleObject;
                if (Math.Sqrt(
                        Math.Pow(Math.Abs(w_clsCircleObject.m_fPosX - p_fPoint.X), 2) + 
                        Math.Pow(Math.Abs(w_clsCircleObject.m_fPosY - p_fPoint.Y), 2)) <= w_clsCircleObject.m_fRadius)
                    w_bRet = true;
            }
            else if (p_clsDrawObject.m_enObjectType == ObjectType.Bubble)
            {
                SpeechBubbleObject w_clsSpeechBubbleObject = p_clsDrawObject.m_clsDrawObject as SpeechBubbleObject;
                if (
                    (p_fPoint.X > w_clsSpeechBubbleObject.m_fPosX - 1 && p_fPoint.X < w_clsSpeechBubbleObject.m_fPosX + w_clsSpeechBubbleObject.m_fWidth + 1) &&
                    (p_fPoint.Y > w_clsSpeechBubbleObject.m_fPosY - 1 && p_fPoint.Y < w_clsSpeechBubbleObject.m_fPosY + w_clsSpeechBubbleObject.m_fHeight + w_clsSpeechBubbleObject.m_fSpeechDepth + 1)
                    )
                    w_bRet = true;
            }

            return w_bRet;
        }


        public static bool PointInTriangle(PointF p, PointF p0, PointF p1, PointF p2)
        {
            var s = (p0.X - p2.X) * (p.Y - p2.Y) - (p0.Y - p2.Y) * (p.X - p2.X);
            var t = (p1.X - p0.X) * (p.Y - p0.Y) - (p1.Y - p0.Y) * (p.X - p0.X);

            if ((s < 0) != (t < 0) && s != 0 && t != 0)
                return false;

            var d = (p2.X - p1.X) * (p.Y - p1.Y) - (p2.Y - p1.Y) * (p.X - p1.X);
            return d == 0 || (d < 0) == (s + t <= 0);
        }

        public static void MoveObject(DrawObject p_clsDrawObject, PointF p_fPoint, float p_fOffsetX, float p_fOffsetY)
        {
            if (p_clsDrawObject.m_enObjectType == ObjectType.Rectangle)
            {
                RectangelObject w_clsRectObj = p_clsDrawObject.m_clsDrawObject as RectangelObject;
                w_clsRectObj.m_fPosX = p_fPoint.X + p_fOffsetX;
                w_clsRectObj.m_fPosY = p_fPoint.Y + p_fOffsetY;

            }
            else if (p_clsDrawObject.m_enObjectType == ObjectType.Circle)
            {
                CircleObject w_clsCircleObj = p_clsDrawObject.m_clsDrawObject as CircleObject;
                w_clsCircleObj.m_fPosX = p_fPoint.X + p_fOffsetX;
                w_clsCircleObj.m_fPosY = p_fPoint.Y + p_fOffsetY;
            }
            else if (p_clsDrawObject.m_enObjectType == ObjectType.Bubble)
            {
                SpeechBubbleObject w_clsSpeechBubbleObj = p_clsDrawObject.m_clsDrawObject as SpeechBubbleObject;

                w_clsSpeechBubbleObj.m_fPosX = p_fPoint.X + p_fOffsetX;
                w_clsSpeechBubbleObj.m_fPosY = p_fPoint.Y + p_fOffsetY;
            }
        }

        public static void MoveAnchor(DrawObject p_clsDrawObject, PointF p_fPoint)
        {
            SpeechBubbleObject w_clsSpeechBubbleObj = p_clsDrawObject.m_clsDrawObject as SpeechBubbleObject;
            w_clsSpeechBubbleObj.m_fSpeechDelta = p_fPoint.X - (w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset);
            w_clsSpeechBubbleObj.m_fSpeechDepth = p_fPoint.Y - (w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight);
        }
    }
}
