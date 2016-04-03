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
using System.Windows;
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

        System.Windows.Threading.DispatcherTimer _timer;
        static int counter;
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
        Model3DGroup KUKA_A2_Counterbalance;
        Model3DGroup KUKA_A2_Shaft;
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

        AxisAngleRotation3D A1_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

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
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = A1_rotateAxis;
            rotateAnimation.To = rotateAxis;

            //apply transformation
            KUKA_A1.Transform = a1_transform;
            a1_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            A1_rotateAxis = rotateAxis;
        }

        #endregion

        #region A2_Axis

        AxisAngleRotation3D A2_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

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
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = A2_rotateAxis;
            rotateAnimation.To = rotateAxis;

            //apply transformation
            KUKA_A2.Transform = a2_transform;
            a2_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            A2_rotateAxis = rotateAxis;
            animate_Counterbalance_angle(angle, seconds);
        }

        #endregion

        #region A3_Axis

        AxisAngleRotation3D A3_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

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
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = A3_rotateAxis;
            rotateAnimation.To = rotateAxis;

            //apply transformation
            KUKA_A3.Transform = a3_transform;
            a3_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            A3_rotateAxis = rotateAxis;
        }
        #endregion

        #region A4_Axis

        AxisAngleRotation3D A4_rotateAxis = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);

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
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = A4_rotateAxis;
            rotateAnimation.To = rotateAxis;

            //apply transformation
            KUKA_A4.Transform = a4_transform;
            a4_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            A4_rotateAxis = rotateAxis;
        }
        #endregion

        #region A5_Axis

        AxisAngleRotation3D A5_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

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
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1),angle);
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = A5_rotateAxis;
            rotateAnimation.To = rotateAxis;
            
           /*   DoubleAnimation angleAnimation = new DoubleAnimation();
               angleAnimation.From = m_A5_angle;
               angleAnimation.To = angle;
               angleAnimation.Duration = new System.Windows.Duration(new TimeSpan(0, 0,seconds)); */
            //apply transformation
            KUKA_A5.Transform = a5_transform;
            a5_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            A5_rotateAxis = rotateAxis;
        }
        #endregion

        #region Counterbalance

        double C_x = 800; //mm
        double C_z = 70.07;
        double Distance_C_Shaft = 604.11;
        double Distance_C_A2 = 800.00;
        double Distance_A2_flange = 200.0;
        //double A2_z = 0;

        AxisAngleRotation3D Counterbalance_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

        double m_Counterbalance_angle;
        public double Counterbalance_angle
        {
            get { return m_Counterbalance_angle; }
            set
            {
                move_Counterbalance_angle(value);
                m_Counterbalance_angle = value;
            }
        }

        void move_Counterbalance_angle(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D Counterbalance_angle_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle));

            //tells where the point of rotation is
            Counterbalance_angle_transform.CenterX = -0.45;
            Counterbalance_angle_transform.CenterY = -0.605;
            Counterbalance_angle_transform.CenterZ = -0.675;

            //apply transformation
            KUKA_A2_Counterbalance.Transform = Counterbalance_angle_transform;


        }

        void animate_Counterbalance_angle(double angle, int seconds)
        {

            //Calculations
            double angle_home_counterbalance = Math.Acos((Math.Pow(Distance_C_Shaft,2)+Math.Pow(Distance_C_A2,2)-Math.Pow( Distance_A2_flange,2))/(2 * Distance_C_Shaft * Distance_C_A2)) ;
            //MessageBox.Show(((Math.Pow(Distance_C_Shaft,2)+Math.Pow(Distance_C_A2,2)-Math.Pow( Distance_A2_flange,2))/(2 * Distance_C_Shaft * Distance_C_A2)).ToString());
            double angle_home_flange = Math.Asin((Math.Sin(angle_home_counterbalance)*Distance_C_Shaft)/ Distance_A2_flange);
            //MessageBox.Show(angle_home_flange.ToString());
            double new_angle_flange = 0;
            double new_angle_counterbalance = 0;
            double new_distance_shaft = 0;
            double new_angle_shaft = 0;
            new_angle_flange = angle_home_flange + (angle*Math.PI/180);
            //MessageBox.Show(new_angle_flange.ToString());
            new_distance_shaft = Math.Sqrt(Math.Pow( Distance_A2_flange, 2) + Math.Pow(Distance_C_A2, 2) - (2 *  Distance_A2_flange * Distance_C_A2 * Math.Cos(new_angle_flange)));
             //MessageBox.Show(new_distance_shaft.ToString());

            new_angle_counterbalance = Math.Asin(((Math.Sin(new_angle_flange) * Distance_A2_flange) / new_distance_shaft));
            new_angle_shaft = 180 - (new_angle_counterbalance * 180 / Math.PI) - new_angle_flange;
            //MessageBox.Show(((Math.Sin(new_angle_flange) * Distance_A2_flange) / new_distance_shaft).ToString());
            // MessageBox.Show(new_angle_counterbalance.ToString());
            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D Counterbalance_transform = new RotateTransform3D();

            //tells where the point of rotation is
            Counterbalance_transform.CenterX = -0.45;
            Counterbalance_transform.CenterY = -0.605;
            Counterbalance_transform.CenterZ = -0.675;

            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), -new_angle_counterbalance * ( 180.0 / Math.PI));
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = Counterbalance_rotateAxis;
            rotateAnimation.To = rotateAxis;

            //apply transformation
            KUKA_A2_Counterbalance.Transform = Counterbalance_transform;
            Counterbalance_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            Counterbalance_rotateAxis = rotateAxis;
            animate_Shaft((-new_angle_counterbalance * (180.0 / Math.PI))-angle, seconds);
        }

        AxisAngleRotation3D Shaft_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

        double m_Shaft_angle;
        public double Shaft_angle
        {
            get { return m_Shaft_angle; }
            set
            {
                move_Shaft(value);
                m_Shaft_angle = value;
            }
        }

        void move_Shaft(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D Shaft_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle));

            //tells where the point of rotation is
            Shaft_transform.CenterX = 0.15;
            Shaft_transform.CenterY = -0.675;
            Shaft_transform.CenterZ = -0.675;

            //apply transformation
            KUKA_A2_Shaft.Transform = Shaft_transform;


        }

        void animate_Shaft(double angle, int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D Shaft_transform = new RotateTransform3D();

            //tells where the point of rotation is
            Shaft_transform.CenterX = 0.15;
            Shaft_transform.CenterY = -0.675;
            Shaft_transform.CenterZ = -0.675;

            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle);
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = Shaft_rotateAxis;
            rotateAnimation.To = rotateAxis;

            //apply transformation
            KUKA_A2_Shaft.Transform = Shaft_transform;
            Shaft_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            Shaft_rotateAxis = rotateAxis;
            //animate_Counterbalance_angle(angle, seconds);
        }

        /*
        TranslateTransform3D Shaft_transform = new TranslateTransform3D(0, 0, 0);

        void animate_shaft(double length, int seconds)
        {
            //animation
            TranslateTransform3D new_Shaft_transform = new TranslateTransform3D(-length,0,0);
           
            
            DoubleAnimation vectorAnimation = new DoubleAnimation();
            vectorAnimation.DecelerationRatio = 0.8;
            vectorAnimation.Duration = TimeSpan.FromSeconds(seconds);
            vectorAnimation.From = Shaft_transform.OffsetX;
            vectorAnimation.To = new_Shaft_transform.OffsetX;

            //apply transformation
            KUKA_A2_Shaft.Transform = Shaft_transform;
            Shaft_transform.BeginAnimation(TranslateTransform3D.OffsetXProperty, vectorAnimation);
            Shaft_transform = new_Shaft_transform;
        }*/

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
            KUKA_A2_Counterbalance = mi.Load(@"Models/KUKA_Counterbalancing_A2.3ds");
            KUKA_A2_Shaft = mi.Load(@"Models/KUKA_Shaft_A2.3ds");

            KUKA_KR90.Children.Add(KUKA_Base);
            KUKA_Base.Children.Add(KUKA_A1);
            KUKA_A1.Children.Add(KUKA_A2);
            KUKA_A1.Children.Add(KUKA_A2_Counterbalance);
            /*************************************/
            KUKA_A2.Children.Add(KUKA_A2_Shaft);
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

            counter = 0;
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 2);
            _timer.Start();
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            

            // animate_a1(-30, 5);
           // KUKA_A1.BeginAnimation(A1_angle, testanimation);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            switch (counter)
            {
                case 0 :
                    animate_a1(60, 2);
                    animate_a2(30, 2);
                    animate_a3(20, 2);
                     animate_a4(20, 2);
                     animate_a5(20, 2);
                     counter++;
                    break;
                case 1 : 
                    animate_a1(30, 2);
                    animate_a2(70, 2);
                    animate_a3(-40, 2);
                     animate_a4(80, 2);
                     animate_a5(80, 2);
                     
                    counter++;
                    break;
                case 2:
                    
                    animate_a1(-80, 2);
                    animate_a2(-30, 2);
                    animate_a3(-20, 2);
                     animate_a4(-70, 2);
                     animate_a5(-20, 2);
                     counter++;
                    break;
                case 3:
                    animate_a1(-20, 2);
                    animate_a2(30, 2);
                    animate_a3(-20, 2);
                     animate_a4(120, 2);
                     animate_a5(-100, 2);
                    counter++;
                    break;
                case 4:

                    counter = 0;
                    break;
            }
        }
    }
}