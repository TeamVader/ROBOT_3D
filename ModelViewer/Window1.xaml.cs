// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Window1.xaml.cs" company="Helix Toolkit">
//   Copyright (c) 2014 Helix Toolkit contributors
// </copyright>
// <summary>
//   Interaction logic for Window1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using HelixToolkit.Wpf;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ModelViewer
{
    /// <summary>
    /// Interaction logic for Window1.
    /// </summary>
    public partial class Window1
    {

        //the small box to find pints in the 3D World
        BoxVisual3D mybox;

        //Binding Model
        public Model3D MyKUKA { get; set; }

        Model3DGroup KUKA_KR90;

        Model3DGroup KUKA_Base;
        Model3DGroup KUKA_A1;
        Model3DGroup KUKA_A2;
        Model3DGroup KUKA_A3;
        Model3DGroup KUKA_A4;
        Model3DGroup KUKA_A5;
        ModelImporter mi = new ModelImporter();

        #region Helper_Box_properties
        public double boxheigth
        {
            get { return mybox.Height; }
            set { mybox.Height = value; }
        }

        public double boxwidth
        {
            get { return mybox.Width; }
            set { mybox.Width = value; }
        }

        public double boxlength
        {
            get { return mybox.Length; }
            set { mybox.Length = value; }
        }


        public double boxX
        {
            get { return mybox.Center.X; }
            set { mybox.Center = new Point3D(value, mybox.Center.Y, mybox.Center.Z); }
        }


        public double boxY
        {
            get { return mybox.Center.Y; }
            set { mybox.Center = new Point3D(mybox.Center.X, value, mybox.Center.Z); }
        }

        public double boxZ
        {
            get { return mybox.Center.Z; }
            set { mybox.Center = new Point3D(mybox.Center.X, mybox.Center.Y, value); }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Window1"/> class.
        /// </summary>
        public Window1()
        {
            this.InitializeComponent();

            KUKA_KR90 = new Model3DGroup();
          
            KUKA_Base= mi.Load(@"Models/KUKA_Base.3ds");
            KUKA_A1= mi.Load(@"Models/KUKA_A1.3ds");
            KUKA_A2= mi.Load(@"Models/KUKA_A2.3ds");
            KUKA_A3= mi.Load(@"Models/KUKA_A3.3ds");
            KUKA_A4= mi.Load(@"Models/KUKA_A4.3ds");
            KUKA_A5= mi.Load(@"Models/KUKA_A5.3ds");

            KUKA_KR90.Children.Add(KUKA_Base);
            KUKA_Base.Children.Add(KUKA_A1);
            KUKA_A1.Children.Add(KUKA_A2);
            KUKA_A2.Children.Add(KUKA_A3);
            KUKA_A3.Children.Add(KUKA_A4);
            KUKA_A4.Children.Add(KUKA_A5);

            //rotate whole skeleton to have it upright
            RotateTransform3D myRotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), -90));
            myRotateTransform.CenterX = 0;
            myRotateTransform.CenterY = 0;
            myRotateTransform.CenterZ = 0;
            KUKA_KR90.Transform = myRotateTransform;

            //instanciate Helper box, uncomment to use it

            mybox = new BoxVisual3D();
            mybox.Height = 0.05;
            mybox.Width = 0.05;
            mybox.Length = 0.05;
            m_helix_viewport.Children.Add(mybox);

            this.MyKUKA = KUKA_KR90;



            Main_Grid.DataContext = this;
        }
    }
}