using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;


namespace FauFau.SDBrowser {

    [Designer(typeof(ScrollbarControlDesigner))]
    public class CustomHScrollbar : UserControl {

        protected Color moChannelColor = Color.Empty;
        protected Image moUpArrowImage = null;
        //protected Image moUpArrowImage_Over = null;
        //protected Image moUpArrowImage_Down = null;
        protected Image moDownArrowImage = null;
        //protected Image moDownArrowImage_Over = null;
        //protected Image moDownArrowImage_Down = null;
        protected Image moThumbArrowImage = null;

        protected Image moThumbTopImage = null;
        protected Image moThumbTopSpanImage = null;
        protected Image moThumbBottomImage = null;
        protected Image moThumbBottomSpanImage = null;
        protected Image moThumbMiddleImage = null;

        protected int moLargeChange = 10;
        protected int moSmallChange = 1;
        protected int moMinimum = 0;
        protected int moMaximum = 100;
        protected int moValue = 0;
        private int nClickPoint;

        protected int moThumbLeft = 0;

        protected bool moAutoSize = false;

        private bool moThumbDown = false;
        private bool moThumbDragging = false;

        public new event EventHandler Scroll = null;
        public event EventHandler ValueChanged = null;

        private int GetThumbHeight()
        {
            int nTrackHeight = (this.Height - (UpArrowImage.Height + DownArrowImage.Height));
            float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
            int nThumbHeight = (int)fThumbHeight;

            if (nThumbHeight > nTrackHeight)
            {
                nThumbHeight = nTrackHeight;
                fThumbHeight = nTrackHeight;
            }
            if (nThumbHeight < 56)
            {
                nThumbHeight = 56;
                fThumbHeight = 56;
            }

            return nThumbHeight;
        }

        public CustomHScrollbar() {

            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            moChannelColor = Color.FromArgb(51, 166, 3);
            UpArrowImage = FauFau.SDBrowser.Properties.Resources.uparrow;
            DownArrowImage = FauFau.SDBrowser.Properties.Resources.downarrow;
            

            ThumbBottomImage = FauFau.SDBrowser.Properties.Resources.ThumbBottom;
            ThumbBottomSpanImage = FauFau.SDBrowser.Properties.Resources.ThumbSpanBottom;
            ThumbTopImage = FauFau.SDBrowser.Properties.Resources.ThumbTop;
            ThumbTopSpanImage = FauFau.SDBrowser.Properties.Resources.ThumbSpanTop;
            ThumbMiddleImage = FauFau.SDBrowser.Properties.Resources.ThumbMiddle;

            moUpArrowImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            moDownArrowImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //moThumbArrowImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            moThumbTopImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            moThumbTopSpanImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            moThumbBottomImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            moThumbBottomSpanImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            moThumbMiddleImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

            this.Height = UpArrowImage.Height;
            base.MinimumSize = new Size(UpArrowImage.Width, UpArrowImage.Height + DownArrowImage.Height + GetThumbHeight());
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("LargeChange")]
        public int LargeChange {
            get { return moLargeChange; }
            set { moLargeChange = value;
            Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("SmallChange")]
        public int SmallChange {
            get { return moSmallChange; }
            set { moSmallChange = value;
            Invalidate();    
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Minimum")]
        public int Minimum {
            get { return moMinimum; }
            set { moMinimum = value;
            Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Maximum")]
        public int Maximum {
            get { return moMaximum; }
            set { moMaximum = value+9;
            Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Value")]
        public int Value {
            get { return moValue; }
            set { moValue = value;

            int nTrackHeight = (this.Height - (UpArrowImage.Height + DownArrowImage.Height));
            float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
            int nThumbHeight = (int)fThumbHeight;

            if (nThumbHeight > nTrackHeight)
            {
                nThumbHeight = nTrackHeight;
                fThumbHeight = nTrackHeight;
            }
            if (nThumbHeight < 56)
            {
                nThumbHeight = 56;
                fThumbHeight = 56;
            }

            //figure out value
            int nPixelRange = nTrackHeight - nThumbHeight;
            int nRealRange = (Maximum - Minimum)-LargeChange;
            float fPerc = 0.0f;
            if (nRealRange != 0)
            {
                fPerc = (float)moValue / (float)nRealRange;
                
            }
            
            float fTop = fPerc * nPixelRange;
            moThumbLeft = (int)fTop;
            

            Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Channel Color")]
        public Color ChannelColor
        {
            get { return moChannelColor; }
            set { moChannelColor = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
        public Image UpArrowImage {
            get { return moUpArrowImage; }
            set { moUpArrowImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
        public Image DownArrowImage {
            get { return moDownArrowImage; }
            set { moDownArrowImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
        public Image ThumbTopImage {
            get { return moThumbTopImage; }
            set { moThumbTopImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
        public Image ThumbTopSpanImage {
            get { return moThumbTopSpanImage; }
            set { moThumbTopSpanImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
        public Image ThumbBottomImage {
            get { return moThumbBottomImage; }
            set { moThumbBottomImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
        public Image ThumbBottomSpanImage {
            get { return moThumbBottomSpanImage; }
            set { moThumbBottomSpanImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
        public Image ThumbMiddleImage {
            get { return moThumbMiddleImage; }
            set { moThumbMiddleImage = value; }
        }

        protected override void OnPaint(PaintEventArgs e) {

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            if (UpArrowImage != null) {
                e.Graphics.DrawImage(UpArrowImage, new Rectangle(new Point(this.Width - UpArrowImage.Width,0), new Size(UpArrowImage.Width, this.Height)));
            }

            Brush oBrush = new SolidBrush(moChannelColor);
            Brush oWhiteBrush = new SolidBrush(Color.FromArgb(255,255,255));
            
            //draw channel left and right border colors
            //e.Graphics.FillRectangle(oWhiteBrush, new Rectangle(0,UpArrowImage.Height, 1, (this.Height-DownArrowImage.Height)));
            //e.Graphics.FillRectangle(oWhiteBrush, new Rectangle(this.Width-1, UpArrowImage.Height, 1, (this.Height - DownArrowImage.Height)));
            
            //draw channel
            e.Graphics.FillRectangle(oBrush, new Rectangle(UpArrowImage.Width, 0, this.Width-DownArrowImage.Width-UpArrowImage.Width, this.Height));

            //draw thumb
            int ntrackWidth = (this.Width - (DownArrowImage.Width + UpArrowImage.Width));
            float fThumbWidth = ((float)LargeChange / (float)Maximum) * ntrackWidth;
            int nThumbHeight = (int)fThumbWidth;

            if (nThumbHeight > ntrackWidth) {
                nThumbHeight = ntrackWidth;
                fThumbWidth = ntrackWidth;
            }
            if (nThumbHeight < 56) {
                nThumbHeight = 56;
                fThumbWidth = 56;
            }

            //Debug.WriteLine(nThumbHeight.ToString());

            float fSpanWidth = (fThumbWidth - (ThumbMiddleImage.Width + ThumbTopImage.Width + ThumbBottomImage.Width)) / 2.0f;
            int nSpanWidth = (int)fSpanWidth;

            int nLeft = moThumbLeft;
            nLeft += UpArrowImage.Width;

            //draw top
            e.Graphics.DrawImage(ThumbTopImage, new Rectangle(nLeft, 1,  ThumbTopImage.Width, this.Height - 2));

            nLeft += ThumbTopImage.Width;
            //draw top span
            Rectangle rect = new Rectangle(nLeft, 1, nSpanWidth, this.Height - 2);


            e.Graphics.DrawImage(ThumbTopSpanImage, (float)nLeft, 1.0f, (float)fSpanWidth * 2, (float)this.Height-2.0f);

            nLeft += nSpanWidth;
            //draw middle
            e.Graphics.DrawImage(ThumbMiddleImage, new Rectangle(nLeft, 1, ThumbMiddleImage.Width, this.Height - 2));


            nLeft += ThumbMiddleImage.Width;
            //draw top span
            rect = new Rectangle(nLeft, 1, nSpanWidth * 2, this.Height - 2);
            e.Graphics.DrawImage(ThumbBottomSpanImage, rect);

            nLeft += nSpanWidth;
            //draw bottom
            e.Graphics.DrawImage(ThumbBottomImage, new Rectangle(nLeft, 1, nSpanWidth, this.Height - 2));

            if (DownArrowImage != null)
            {
                e.Graphics.DrawImage(DownArrowImage, new Rectangle(new Point(0, 0), new Size(DownArrowImage.Width, this.Height)));
            }

        }

        public override bool AutoSize {
            get {
                return base.AutoSize;
            }
            set {
                base.AutoSize = value;
                if (base.AutoSize) {
                    this.Width = moUpArrowImage.Width;
                }
            }
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // CustomScrollbar
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
            this.Name = "CustomScrollbar";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseUp);
            this.ResumeLayout(false);

        }

        private void CustomScrollbar_MouseDown(object sender, MouseEventArgs e) {
            Point ptPoint = this.PointToClient(Cursor.Position);
            int nTrackWidth = (this.Width - (UpArrowImage.Width + DownArrowImage.Width));
            float fThumbWidth = ((float)LargeChange / (float)Maximum) * nTrackWidth;
            int nThumbWidth = (int)fThumbWidth;

            if (nThumbWidth > nTrackWidth) {
                nThumbWidth = nTrackWidth;
                fThumbWidth = nTrackWidth;
            }
            if (nThumbWidth < 56) {
                nThumbWidth = 56;
                fThumbWidth = 56;
            }

            int nLeft = moThumbLeft;
            nLeft += UpArrowImage.Width;


            Rectangle thumbrect = new Rectangle(new Point(nLeft, 1), new Size(nThumbWidth, ThumbMiddleImage.Height));
            if (thumbrect.Contains(ptPoint))
            {
                
                //hit the thumb
                nClickPoint = (nLeft - ptPoint.X);
                //MessageBox.Show(Convert.ToString((ptPoint.Y - nTop)));
                this.moThumbDown = true;
            }

            Rectangle uparrowrect = new Rectangle(new Point(0, 1), new Size(UpArrowImage.Height, UpArrowImage.Width));
            if (uparrowrect.Contains(ptPoint))
            {

                int nRealRange = (Maximum - Minimum)-LargeChange;
                int nPixelRange = (nTrackWidth - nThumbWidth);
                if (nRealRange > 0)
                {
                    if (nPixelRange > 0)
                    {
                        if ((moThumbLeft - SmallChange) < 0)
                            moThumbLeft = 0;
                        else
                            moThumbLeft -= SmallChange;

                        //figure out value
                        float fPerc = (float)moThumbLeft / (float)nPixelRange;
                        float fValue = fPerc * (Maximum - LargeChange);
                        
                            moValue = (int)fValue;
                        Debug.WriteLine(moValue.ToString());

                        if (ValueChanged != null)
                            ValueChanged(this, new EventArgs());

                        if (Scroll != null)
                            Scroll(this, new EventArgs());

                        Invalidate();
                    }
                }
            }

            Rectangle downarrowrect = new Rectangle(new Point(UpArrowImage.Width+nTrackWidth, 1), new Size(UpArrowImage.Width, UpArrowImage.Height));
            if (downarrowrect.Contains(ptPoint))
            {
                int nRealRange = (Maximum - Minimum) - LargeChange;
                int nPixelRange = (nTrackWidth - nThumbWidth);
                if (nRealRange > 0)
                {
                    if (nPixelRange > 0)
                    {
                        if ((moThumbLeft + SmallChange) > nPixelRange)
                            moThumbLeft = nPixelRange;
                        else
                            moThumbLeft += SmallChange;

                        //figure out value
                        float fPerc = (float)moThumbLeft / (float)nPixelRange;
                        float fValue = fPerc * (Maximum-LargeChange);
                       
                            moValue = (int)fValue;
                        Debug.WriteLine(moValue.ToString());

                        if (ValueChanged != null)
                            ValueChanged(this, new EventArgs());

                        if (Scroll != null)
                            Scroll(this, new EventArgs());

                        Invalidate();
                    }
                }
            }
        }

        private void CustomScrollbar_MouseUp(object sender, MouseEventArgs e) {
            this.moThumbDown = false;
            this.moThumbDragging = false;
        }

        private void MoveThumb(int x) {
            int nRealRange = Maximum - Minimum;
            int nTrackWidth = (this.Width - (UpArrowImage.Width + DownArrowImage.Width));
            float fThumbWidth = ((float)LargeChange / (float)Maximum) * nTrackWidth;
            int nThumbWidth = (int)fThumbWidth;

            if (nThumbWidth > nTrackWidth) {
                nThumbWidth = nTrackWidth;
                fThumbWidth = nTrackWidth;
            }
            if (nThumbWidth < 56) {
                nThumbWidth = 56;
                fThumbWidth = 56;
            }

            int nSpot = nClickPoint;

            int nPixelRange = (nTrackWidth - nThumbWidth);
            if (moThumbDown && nRealRange > 0) {
                if (nPixelRange > 0) {
                    int nNewThumbLeft = x - (UpArrowImage.Width + nSpot + nThumbWidth);
                    
                    if(nNewThumbLeft<0)
                    {
                        moThumbLeft = nNewThumbLeft = 0;
                    }
                    else if(nNewThumbLeft > nPixelRange)
                    {
                        moThumbLeft = nNewThumbLeft = nPixelRange;
                    }
                    else {
                        moThumbLeft = nNewThumbLeft;
                    }
                   
                    //figure out value
                    float fPerc = (float)moThumbLeft / (float)nPixelRange;
                    float fValue = fPerc * (Maximum-LargeChange);
                    moValue = (int)fValue;
                    Debug.WriteLine(moValue.ToString());

                    Application.DoEvents();

                    Invalidate();
                }
            }
        }

        private void CustomScrollbar_MouseMove(object sender, MouseEventArgs e) {
            if(moThumbDown == true)
            {
                this.moThumbDragging = true;
            }

            if (this.moThumbDragging) {

                MoveThumb(e.X);
            }

            if(ValueChanged != null)
                ValueChanged(this, new EventArgs());

            if(Scroll != null)
                Scroll(this, new EventArgs());
        }

    }


}