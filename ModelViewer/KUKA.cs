using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace ModelViewer
{
    public partial class Window1
    {
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

        void animate_a1(double angle, int seconds)
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
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle);
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
            double angle_home_counterbalance = Math.Acos((Math.Pow(Distance_C_Shaft, 2) + Math.Pow(Distance_C_A2, 2) - Math.Pow(Distance_A2_flange, 2)) / (2 * Distance_C_Shaft * Distance_C_A2));
            //MessageBox.Show(((Math.Pow(Distance_C_Shaft,2)+Math.Pow(Distance_C_A2,2)-Math.Pow( Distance_A2_flange,2))/(2 * Distance_C_Shaft * Distance_C_A2)).ToString());
            double angle_home_flange = Math.Asin((Math.Sin(angle_home_counterbalance) * Distance_C_Shaft) / Distance_A2_flange);
            //MessageBox.Show(angle_home_flange.ToString());
            double new_angle_flange = 0;
            double new_angle_counterbalance = 0;
            double new_distance_shaft = 0;
            double new_angle_shaft = 0;
            new_angle_flange = angle_home_flange + (angle * Math.PI / 180);
            //MessageBox.Show(new_angle_flange.ToString());
            new_distance_shaft = Math.Sqrt(Math.Pow(Distance_A2_flange, 2) + Math.Pow(Distance_C_A2, 2) - (2 * Distance_A2_flange * Distance_C_A2 * Math.Cos(new_angle_flange)));
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
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), -new_angle_counterbalance * (180.0 / Math.PI));
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = Counterbalance_rotateAxis;
            rotateAnimation.To = rotateAxis;

            //apply transformation
            KUKA_A2_Counterbalance.Transform = Counterbalance_transform;
            Counterbalance_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            Counterbalance_rotateAxis = rotateAxis;
            animate_Shaft((-new_angle_counterbalance * (180.0 / Math.PI)) - angle, seconds);
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
    }
}
