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
        public Model3D MyStaubli { get; set; }

        Model3DGroup Staubli_TX60;

        Model3DGroup Staubli_Base;
        Model3DGroup Staubli_A1;
        Model3DGroup Staubli_A2;
        Model3DGroup Staubli_A3;
        Model3DGroup Staubli_A4;
        Model3DGroup Staubli_A5;
        Model3DGroup Staubli_A6;
        

        #region Angle_Bindings_Axis

        #region A1_Axis

        AxisAngleRotation3D Staubli_A1_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

        double Staubli_m_A1_angle;
        public double Staubli_A1_angle
        {
            get { return Staubli_m_A1_angle; }
            set
            {
                Staubli_move_a1(value);
                Staubli_m_A1_angle = value;
            }
        }

        void Staubli_move_a1(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a1_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle));

            //tells where the point of rotation is
            a1_transform.CenterX = 0.0334;
            a1_transform.CenterY = -0.0448;
            a1_transform.CenterZ = 1;

            //apply transformation
            Staubli_A1.Transform = a1_transform;


        }

        void Staubli_animate_a1(double angle, int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a1_transform = new RotateTransform3D();

            //tells where the point of rotation is
            a1_transform.CenterX = 0.0334;
            a1_transform.CenterY = -0.0448;
            a1_transform.CenterZ = 1;

            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle);
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = Staubli_A1_rotateAxis;
            rotateAnimation.To = rotateAxis;

            //apply transformation
            Staubli_A1.Transform = a1_transform;
            a1_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            Staubli_A1_rotateAxis = rotateAxis;
        }

        #endregion

        #region A2_Axis

        AxisAngleRotation3D Staubli_A2_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

        double Staubli_m_A2_angle;
        public double Staubli_A2_angle
        {
            get { return Staubli_m_A2_angle; }
            set
            {
                Staubli_move_a2(value);
                Staubli_m_A2_angle = value;
            }
        }

        void Staubli_move_a2(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a2_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle));

            //tells where the point of rotation is
            a2_transform.CenterX = 0.0334;
            a2_transform.CenterY = 0.0711;
            a2_transform.CenterZ = 0.3230;

            //apply transformation
            Staubli_A2.Transform = a2_transform;


        }

        void Staubli_animate_a2(double angle, int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a2_transform = new RotateTransform3D();

            //tells where the point of rotation is
            a2_transform.CenterX = 0.0334;
            a2_transform.CenterY = 0.0711;
            a2_transform.CenterZ = 0.3230;

            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle);
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = Staubli_A2_rotateAxis;
            rotateAnimation.To = rotateAxis;

            //apply transformation
            Staubli_A2.Transform = a2_transform;
            a2_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            Staubli_A2_rotateAxis = rotateAxis;
            
        }

        #endregion

        #region A3_Axis

        AxisAngleRotation3D Staubli_A3_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

        double Staubli_m_A3_angle;
        public double Staubli_A3_angle
        {
            get { return Staubli_m_A3_angle; }
            set
            {
                Staubli_move_a3(value);
                Staubli_m_A3_angle = value;
            }
        }

        void Staubli_move_a3(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a3_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle));

            //tells where the point of rotation is
            a3_transform.CenterX = 0.0334;
            a3_transform.CenterY = 0.0711;
            a3_transform.CenterZ = 0.5727;

            //apply transformation
            Staubli_A3.Transform = a3_transform;


        }

        void Staubli_animate_a3(double angle, int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a3_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle));

            //tells where the point of rotation is
            a3_transform.CenterX = 0.0334;
            a3_transform.CenterY = 0.0711;
            a3_transform.CenterZ = 0.5727;

            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle);
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = Staubli_A3_rotateAxis;
            rotateAnimation.To = rotateAxis;

            //apply transformation
            Staubli_A3.Transform = a3_transform;
            a3_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            Staubli_A3_rotateAxis = rotateAxis;
        }
        #endregion

        #region A4_Axis

        AxisAngleRotation3D Staubli_A4_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

        double Staubli_m_A4_angle;
        public double Staubli_A4_angle
        {
            get { return Staubli_m_A4_angle; }
            set
            {
                Staubli_move_a4(value);
                Staubli_m_A4_angle = value;
            }
        }

        void Staubli_move_a4(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a4_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle));

            //tells where the point of rotation is
            a4_transform.CenterX = 0.0334;
            a4_transform.CenterY = -0.0276;
            a4_transform.CenterZ = 0.6636;

            //apply transformation
            Staubli_A4.Transform = a4_transform;


        }

        void Staubli_animate_a4(double angle, int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a4_transform = new RotateTransform3D();

            //tells where the point of rotation is
            a4_transform.CenterX = 0.0334;
            a4_transform.CenterY = -0.0276;
            a4_transform.CenterZ = 0.6636;


            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle);
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = Staubli_A4_rotateAxis;
            rotateAnimation.To = rotateAxis;

            //apply transformation
            Staubli_A4.Transform = a4_transform;
            a4_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            Staubli_A4_rotateAxis = rotateAxis;
        }
        #endregion

        #region A5_Axis

        AxisAngleRotation3D Staubli_A5_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

        double Staubli_m_A5_angle;
        public double Staubli_A5_angle
        {
            get { return Staubli_m_A5_angle; }
            set
            {
                Staubli_move_a5(value);
                Staubli_m_A5_angle = value;
            }
        }

        void Staubli_move_a5(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a5_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle));

            //tells where the point of rotation is
            a5_transform.CenterX = 0.0334;
            a5_transform.CenterY = -0.0090;
            a5_transform.CenterZ = 0.8397;

            //apply transformation
            Staubli_A5.Transform = a5_transform;


        }

        void Staubli_animate_a5(double angle, int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a5_transform = new RotateTransform3D();

            //tells where the point of rotation is
            a5_transform.CenterX = 0.0334;
            a5_transform.CenterY = -0.0090;
            a5_transform.CenterZ = 0.8397;


            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle);
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
            Staubli_A5.Transform = a5_transform;
            a5_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            A5_rotateAxis = rotateAxis;
        }
        #endregion

        #region A6_Axis

        AxisAngleRotation3D Staubli_A6_rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

        double Staubli_m_A6_angle;
        public double Staubli_A6_angle
        {
            get { return Staubli_m_A6_angle; }
            set
            {
                Staubli_move_a6(value);
                Staubli_m_A6_angle = value;
            }
        }

        void Staubli_move_a6(double angle)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a6_transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle));

            //tells where the point of rotation is
            a6_transform.CenterX = 0.0334;
            a6_transform.CenterY = -0.0276;
            a6_transform.CenterZ = 0.9000;

            //apply transformation
            Staubli_A6.Transform = a6_transform;


        }

        void Staubli_animate_a6(double angle, int seconds)
        {

            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D a6_transform = new RotateTransform3D();

            //tells where the point of rotation is
            a6_transform.CenterX = 0.0334;
            a6_transform.CenterY = -0.0276;
            a6_transform.CenterZ = 0.9000;


            //animation
            AxisAngleRotation3D rotateAxis = new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle);
            Rotation3DAnimation rotateAnimation = new Rotation3DAnimation();
            rotateAnimation.DecelerationRatio = 0.8;
            rotateAnimation.Duration = TimeSpan.FromSeconds(seconds);
            rotateAnimation.From = Staubli_A6_rotateAxis;
            rotateAnimation.To = rotateAxis;

            /*   DoubleAnimation angleAnimation = new DoubleAnimation();
                angleAnimation.From = m_A5_angle;
                angleAnimation.To = angle;
                angleAnimation.Duration = new System.Windows.Duration(new TimeSpan(0, 0,seconds)); */
            //apply transformation
            Staubli_A6.Transform = a6_transform;
            a6_transform.BeginAnimation(RotateTransform3D.RotationProperty, rotateAnimation);
            Staubli_A6_rotateAxis = rotateAxis;
        }
        #endregion

        #endregion
    }
}
