using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Font = Microsoft.Maui.Graphics.Font;

namespace DrawingCanvas.Drawables
{
    internal class GraphicsDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.Black;
            canvas.DrawRectangle(dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height);

            float w_fFontSize = 20;
            Font w_clsFont = new Font("Arial");
            canvas.Font = w_clsFont;
            canvas.FontSize = w_fFontSize;

            canvas.StrokeSize = 2;

            IPattern pattern;


            for (int i = 0; i < DrawObjects.m_objDrawObjectList.Count; i++)
            {
                PathF w_clsPath = new PathF();

                if (DrawObjects.m_objDrawObjectList[i].m_enObjectType == ObjectType.Rectangle)
                {
                    RectangelObject w_clsRectangleObj = DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as RectangelObject;
                    w_clsPath.AppendRectangle(w_clsRectangleObj.m_fPosX, w_clsRectangleObj.m_fPosY, w_clsRectangleObj.m_fWidth, w_clsRectangleObj.m_fHeight);

                    canvas.StrokeColor = w_clsRectangleObj.m_enBorderColor;
                    canvas.FillColor = w_clsRectangleObj.m_enBackColor;

                    canvas.FillPath(w_clsPath);
                    canvas.DrawPath(w_clsPath);

                    // Draw Selected State
                    if (DrawObjects.m_nSelObjIdx == i)
                    {
                        canvas.StrokeSize = 4;
                        canvas.StrokeColor = Colors.Silver;
                        canvas.DrawRectangle(w_clsRectangleObj.m_fPosX - 4, w_clsRectangleObj.m_fPosY - 4, w_clsRectangleObj.m_fWidth + 8, w_clsRectangleObj.m_fHeight + 8);
                        canvas.StrokeSize = 2;

                        /*
                        // Create a 10x10 template for the pattern
                        using (PictureCanvas picture = new PictureCanvas(0, 0, 10, 10))
                        {
                            picture.StrokeColor = Colors.Silver;
                            picture.DrawLine(0, 0, 10, 10);
                            picture.DrawLine(0, 10, 10, 0);
                            pattern = new PicturePattern(picture.Picture, 10, 10);
                        }

                        // Fill the rectangle with the 10x10 pattern
                        PatternPaint patternPaint = new PatternPaint
                        {
                            Pattern = pattern
                        };
                        canvas.SetFillPaint(patternPaint, RectF.Zero);
                        canvas.FillRectangle(w_clsRectangleObj.m_fPosX, w_clsRectangleObj.m_fPosY, w_clsRectangleObj.m_fWidth, w_clsRectangleObj.m_fHeight);
                        */
                    }
                }
                else if (DrawObjects.m_objDrawObjectList[i].m_enObjectType == ObjectType.Circle)
                {
                    CircleObject w_clsCircleObj = DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as CircleObject;
                    w_clsPath.AppendCircle(w_clsCircleObj.m_fPosX, w_clsCircleObj.m_fPosY, w_clsCircleObj.m_fRadius);

                    canvas.StrokeColor = w_clsCircleObj.m_enBorderColor;
                    canvas.FillColor = w_clsCircleObj.m_enBackColor;

                    canvas.FillPath(w_clsPath);
                    canvas.DrawPath(w_clsPath);

                    // Draw Selected State
                    if (DrawObjects.m_nSelObjIdx == i)
                    {
                        canvas.StrokeSize = 4;
                        canvas.StrokeColor = Colors.Silver;
                        canvas.DrawRectangle(
                            w_clsCircleObj.m_fPosX - w_clsCircleObj.m_fRadius - 4,
                            w_clsCircleObj.m_fPosY - w_clsCircleObj.m_fRadius - 4,
                           w_clsCircleObj.m_fRadius * 2 + 8, w_clsCircleObj.m_fRadius * 2 + 8);
                        canvas.StrokeSize = 2;
                        /*
                        // Create a 10x10 template for the pattern
                        using (PictureCanvas picture = new PictureCanvas(0, 0, 10, 10))
                        {
                            picture.StrokeColor = Colors.Silver;
                            picture.DrawLine(0, 0, 10, 10);
                            picture.DrawLine(0, 10, 10, 0);
                            pattern = new PicturePattern(picture.Picture, 10, 10);
                        }

                        // Fill the rectangle with the 10x10 pattern
                        PatternPaint patternPaint = new PatternPaint
                        {
                            Pattern = pattern
                        };
                        canvas.SetFillPaint(patternPaint, RectF.Zero);
                        canvas.FillRectangle(
                            w_clsCircleObj.m_fPosX - w_clsCircleObj.m_fRadius, 
                            w_clsCircleObj.m_fPosY - w_clsCircleObj.m_fRadius,
                            w_clsCircleObj.m_fRadius * 2, w_clsCircleObj.m_fRadius * 2);
                        */
                    }
                }
                else if (DrawObjects.m_objDrawObjectList[i].m_enObjectType == ObjectType.Bubble)
                {
                    SpeechBubbleObject w_clsSpeechBubbleObj = DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as SpeechBubbleObject;


                    SizeF stringSize = canvas.GetStringSize(w_clsSpeechBubbleObj.m_strSpeech, w_clsFont, w_fFontSize);
                    float w_fTextWidth = stringSize.Width;
                    float w_fTextHeight = stringSize.Height;

                    w_clsSpeechBubbleObj.m_fWidth = w_fTextWidth + 30;
                    w_clsSpeechBubbleObj.m_fHeight = w_fTextHeight + 30;


                    canvas.StrokeColor = w_clsSpeechBubbleObj.m_enBorderColor;
                    canvas.FillColor = w_clsSpeechBubbleObj.m_enBackColor;

                    w_clsPath.MoveTo(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fCornerRadius, w_clsSpeechBubbleObj.m_fPosY);
                    w_clsPath.AddArc(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fWidth - w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fPosY,
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fWidth,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fCornerRadius,
                        90, 0, true);

                    w_clsPath.AddArc(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fWidth - w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight - w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fWidth,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight,
                        0, -90, true);


                    w_clsPath.LineTo(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset + w_clsSpeechBubbleObj.m_fSpeechWidth,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight);
                    w_clsPath.LineTo(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset + w_clsSpeechBubbleObj.m_fSpeechDelta,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight + w_clsSpeechBubbleObj.m_fSpeechDepth);
                    w_clsPath.LineTo(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight);


                    w_clsPath.AddArc(w_clsSpeechBubbleObj.m_fPosX,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight - w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight,
                        -90, -180, true);
                    w_clsPath.AddArc(w_clsSpeechBubbleObj.m_fPosX,
                        w_clsSpeechBubbleObj.m_fPosY,
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fCornerRadius,
                        -180, 90, true);
                    w_clsPath.Close();

                    canvas.FillPath(w_clsPath);
                    canvas.DrawPath(w_clsPath);

                    canvas.DrawString(w_clsSpeechBubbleObj.m_strSpeech,
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fWidth - w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        w_clsSpeechBubbleObj.m_fHeight - w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        HorizontalAlignment.Left, VerticalAlignment.Top);

                    // Draw Selected State
                    if (DrawObjects.m_nSelObjIdx == i)
                    {
                        canvas.StrokeSize = 4;
                        canvas.StrokeColor = Colors.Silver;
                        canvas.DrawRectangle(
                            w_clsSpeechBubbleObj.m_fPosX - 4,
                            w_clsSpeechBubbleObj.m_fPosY - 4,
                            w_clsSpeechBubbleObj.m_fWidth + 8, w_clsSpeechBubbleObj.m_fHeight + w_clsSpeechBubbleObj.m_fSpeechDepth + 8);
                        canvas.StrokeSize = 2;

                        /*
                        // Create a 10x10 template for the pattern
                        using (PictureCanvas picture = new PictureCanvas(0, 0, 10, 10))
                        {
                            picture.StrokeColor = Colors.Silver;
                            picture.DrawLine(0, 0, 10, 10);
                            picture.DrawLine(0, 10, 10, 0);
                            pattern = new PicturePattern(picture.Picture, 10, 10);
                        }

                        // Fill the rectangle with the 10x10 pattern
                        PatternPaint patternPaint = new PatternPaint
                        {
                            Pattern = pattern
                        };
                        canvas.SetFillPaint(patternPaint, RectF.Zero);
                        canvas.FillRectangle(w_clsSpeechBubbleObj.m_fPosX, w_clsSpeechBubbleObj.m_fPosY, 
                            w_clsSpeechBubbleObj.m_fWidth, w_clsSpeechBubbleObj.m_fHeight + w_clsSpeechBubbleObj.m_fSpeechDepth);
                        */
                    }                    
                }
            }
        }

        /*public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float w_fFontSize = 20;
            Font w_clsFont = new Font("Arial");
            canvas.Font = w_clsFont;
            canvas.FontSize = w_fFontSize;

            canvas.StrokeSize = 2;


            for (int i = 0; i < DrawObjects.m_objDrawObjectList.Count; i++)
            {
                if(DrawObjects.m_objDrawObjectList[i].m_enObjectType == ObjectType.Rectangle)
                {
                    RectangelObject w_clsRectangleObj = DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as RectangelObject;
                    canvas.StrokeColor = w_clsRectangleObj.m_enColor;
                    canvas.DrawRectangle(w_clsRectangleObj.m_fPosX, w_clsRectangleObj.m_fPosY, w_clsRectangleObj.m_fWidth, w_clsRectangleObj.m_fHeight);
                }
                else if(DrawObjects.m_objDrawObjectList[i].m_enObjectType == ObjectType.Circle)
                {
                    CircleObject w_clsCircleObj = DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as CircleObject;
                    canvas.StrokeColor = w_clsCircleObj.m_enColor;
                    canvas.DrawEllipse(w_clsCircleObj.m_fPosX - w_clsCircleObj.m_fRadius, w_clsCircleObj.m_fPosY - w_clsCircleObj.m_fRadius, 
                        w_clsCircleObj.m_fRadius * 2, w_clsCircleObj.m_fRadius * 2);
                }
                else if(DrawObjects.m_objDrawObjectList[i].m_enObjectType == ObjectType.Bubble)
                {
                    SpeechBubbleObject w_clsSpeechBubbleObj = DrawObjects.m_objDrawObjectList[i].m_clsDrawObject as SpeechBubbleObject;


                    SizeF stringSize = canvas.GetStringSize(w_clsSpeechBubbleObj.m_strSpeech, w_clsFont, w_fFontSize);
                    float w_fTextWidth = stringSize.Width;
                    float w_fTextHeight = stringSize.Height;


                    w_clsSpeechBubbleObj.m_fWidth = w_fTextWidth + 30;
                    w_clsSpeechBubbleObj.m_fHeight = w_fTextHeight + 30;

                    canvas.StrokeColor = w_clsSpeechBubbleObj.m_enColor;
                    canvas.DrawLine(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fCornerRadius - 1, 
                        w_clsSpeechBubbleObj.m_fPosY,
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fWidth - w_clsSpeechBubbleObj.m_fCornerRadius, 
                        w_clsSpeechBubbleObj.m_fPosY);
                    canvas.DrawArc(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fWidth - w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        w_clsSpeechBubbleObj.m_fPosY,
                        w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        90, 0, true, false);
                    canvas.DrawLine(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fWidth,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fCornerRadius - 1,
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fWidth,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight - w_clsSpeechBubbleObj.m_fCornerRadius);
                    canvas.DrawArc(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fWidth - w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight - w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        0, -90, true, false);
                    canvas.DrawLine(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fWidth - w_clsSpeechBubbleObj.m_fCornerRadius + 1,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight,
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset + w_clsSpeechBubbleObj.m_fSpeechWidth,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight);

                    canvas.DrawLine(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset + w_clsSpeechBubbleObj.m_fSpeechWidth,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight,
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight + w_clsSpeechBubbleObj.m_fSpeechDepth);
                    canvas.DrawLine(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight,
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight + w_clsSpeechBubbleObj.m_fSpeechDepth);
                    canvas.DrawLine(w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fSpeechOffset,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight,
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight);

                    canvas.DrawArc(w_clsSpeechBubbleObj.m_fPosX,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight - w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        -90, -180, true, false);
                    canvas.DrawLine(w_clsSpeechBubbleObj.m_fPosX,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fPosX,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fHeight - w_clsSpeechBubbleObj.m_fCornerRadius + 1);
                    canvas.DrawArc(w_clsSpeechBubbleObj.m_fPosX,
                        w_clsSpeechBubbleObj.m_fPosY,
                        w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        -180, 90, true, false);


                    


                    canvas.DrawString(w_clsSpeechBubbleObj.m_strSpeech, 
                        w_clsSpeechBubbleObj.m_fPosX + w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fPosY + w_clsSpeechBubbleObj.m_fCornerRadius,
                        w_clsSpeechBubbleObj.m_fWidth - w_clsSpeechBubbleObj.m_fCornerRadius * 2,
                        w_clsSpeechBubbleObj.m_fHeight - w_clsSpeechBubbleObj.m_fCornerRadius * 2, 
                        HorizontalAlignment.Center, VerticalAlignment.Top);
                }
            }
        }*/
    }
}