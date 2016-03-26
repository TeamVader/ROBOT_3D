// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Window1.xaml.cs" company="Helix Toolkit">
//   Copyright (c) 2014 Helix Toolkit contributors
// </copyright>
// <summary>
//   Interaction logic for Window1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using HelixToolkit.Wpf;
using System;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Animation;
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

        #region Angle_Bindings_Axis

        #region A1_Axis

        double m_A1_angle;
        public double A1_angle
        {
            get { return m_A1_angle; }
            set
            {
                move_a1(value);
                m_A1_angle = value;
            }
        }

        void move_a1(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a1_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle));

            //tells where the point of rotation is
            a1_transform.CenterX = 0;
            a1_transform.CenterY = 1;
            a1_transform.CenterZ = 0;

            //apply transformation
            KUKA_A1.Transform = a1_transform;

          
        }

        void animate_a1(double angle,int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a1_transform = new RotateTransform3D();

            //tells where the point of rotation is
            a1_transform.CenterX = 0;
            a1_transform.CenterY = 1;
            a1_transform.CenterZ = 0;

            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle);
            Rotation3DAnimation rotateAnimation =new Rotation3DAnimation(rotateAxis, TimeSpan.FromSeconds(seconds));
            rotateAnimation.DecelerationRatio = 0.8;

            //apply transformation
            KUKA_A1.Transform = a1_transform;
            a1_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);

        }

        #endregion

        #region A2_Axis
        double m_A2_angle;
        public double A2_angle
        {
            get { return m_A2_angle; }
            set
            {
                move_a2(value);
                m_A2_angle = value;
            }
        }

        void move_a2(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a2_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle));

            //tells where the point of rotation is
            a2_transform.CenterX = 0.35;
            a2_transform.CenterY = -0.675;
            a2_transform.CenterZ = -0.675;

            //apply transformation
            KUKA_A2.Transform = a2_transform;


        }

        void animate_a2(double angle, int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a2_transform = new RotateTransform3D();

            //tells where the point of rotation is
            a2_transform.CenterX = 0.35;
            a2_transform.CenterY = -0.675;
            a2_transform.CenterZ = -0.675;

            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle);
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation(rotateAxis, TimeSpan.FromSeconds(seconds));
            rotateAnimation.DecelerationRatio = 0.8;

            //apply transformation
            KUKA_A2.Transform = a2_transform;
            a2_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);

        }

        #endregion

        #region A3_Axis
        double m_A3_angle;
        public double A3_angle
        {
            get { return m_A3_angle; }
            set
            {
                move_a3(value);
                m_A3_angle = value;
            }
        }

        void move_a3(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a3_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle));

            //tells where the point of rotation is
            a3_transform.CenterX = 0.350;
            a3_transform.CenterY = -1.825;
            a3_transform.CenterZ = -0.675;

            //apply transformation
            KUKA_A3.Transform = a3_transform;


        }

        void animate_a3(double angle, int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a3_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle));

            //tells where the point of rotation is
            a3_transform.CenterX = 0.350;
            a3_transform.CenterY = -1.825;
            a3_transform.CenterZ = -0.675;

            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle);
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation(rotateAxis, TimeSpan.FromSeconds(seconds));
            rotateAnimation.DecelerationRatio = 0.8;

            //apply transformation
            KUKA_A3.Transform = a3_transform;
            a3_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);

        }
        #endregion

        #region A4_Axis

        double m_A4_angle;
        public double A4_angle
        {
            get { return m_A4_angle; }
            set
            {
                move_a4(value);
                m_A4_angle = value;
            }
        }

        void move_a4(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a4_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), angle));

            //tells where the point of rotation is
            a4_transform.CenterX = 1.13;
            a4_transform.CenterY = -1.7840;
            a4_transform.CenterZ = -0.00;

            //apply transformation
            KUKA_A4.Transform = a4_transform;


        }

        void animate_a4(double angle, int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a4_transform = new RotateTransform3D();

            //tells where the point of rotation is
            a4_transform.CenterX = 1.13;
            a4_transform.CenterY = -1.7840;
            a4_transform.CenterZ = -0.00;


            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(1, 0, 0), angle);
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation(rotateAxis, TimeSpan.FromSeconds(seconds));
            rotateAnimation.DecelerationRatio = 0.8;

            //apply transformation
            KUKA_A4.Transform = a4_transform;
            a4_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);

        }
        #endregion

        #region A5_Axis
        double m_A5_angle;
        public double A5_angle
        {
            get { return m_A5_angle; }
            set
            {
                move_a5(value);
                m_A5_angle = value;
            }
        }

        void move_a5(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a5_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle));

            //tells where the point of rotation is
            a5_transform.CenterX = 1.55;
            a5_transform.CenterY = -1.784;
            a5_transform.CenterZ = -0.04;

            //apply transformation
            KUKA_A5.Transform = a5_transform;


        }

        void animate_a5(double angle, int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a5_transform = new RotateTransform3D();

            //tells where the point of rotation is
            a5_transform.CenterX = 1.55;
            a5_transform.CenterY = -1.784;
            a5_transform.CenterZ = -0.04;


            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle);
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation(rotateAxis, TimeSpan.FromSeconds(seconds));
            rotateAnimation.DecelerationRatio = 0.8;

            //apply transformation
            KUKA_A5.Transform = a5_transform;
            a5_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);

        }
        #endregion

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
            mybox.Height = 0.01;
            mybox.Width = 0.01;
            mybox.Length = 0.01;
            m_helix_viewport.Children.Add(mybox);

            this.MyKUKA = KUKA_KR90;


            
            Main_Grid.DataContext = this;
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            animate_a1(30, 5);
            animate_a2(30, 5);
            animate_a3(20, 4);
            animate_a4(20, 4);
            animate_a5(20, 4);

            // animate_a1(-30, 5);
           // KUKA_A1.BeginAnimation(A1_angle, testanimation);
        }
    }
}