using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace MarsImageViewer
{
    public class AccelerometerShake : IDisposable
    {
        private const double ShakeThreshold = 0.5;
        private readonly Accelerometer _sensor = new Accelerometer();
        private AccelerometerReading _lastReading;
        private int _shakeCount;
        private bool _shaking;
        private bool isHaveLastReading = false;

        public AccelerometerShake()
        {
            Accelerometer sensor = new Accelerometer();
            if (sensor.State == SensorState.NotSupported)
                throw new NotSupportedException("Accelerometer not supported on this device");
            _sensor = sensor;
        }

        public SensorState State
        {
            get { return _sensor.State; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_sensor != null)
                _sensor.Dispose();
        }

        #endregion

        public delegate void ShakeEventHandler(object sender, ShakeStateEventArgs e);

        private event ShakeEventHandler ShakeDetectedHandler;
        public event ShakeEventHandler ShakeDetected
        {
            add
            {
                ShakeDetectedHandler += value;
                _sensor.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(Shake_CurrentValueChanged);
            }
            remove
            {
                ShakeDetectedHandler -= value;

                //_sensor.ReadingChanged -= ReadingChanged;
                _sensor.CurrentValueChanged -= Shake_CurrentValueChanged;
            }
        }

        public void Start()
        {
            if (_sensor != null)
                _sensor.Start();
        }

        public void Stop()
        {
            if (_sensor != null)
                _sensor.Stop();
        }

        private void Shake_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            //Code for checking shake detection
            //if (_sensor.State == SensorState.Ready)
            {
                AccelerometerReading reading = e.SensorReading;

                if (System.Diagnostics.Debugger.IsAttached)
                {
                    bool bCompare = reading.Equals(_lastReading);
                    string outString = String.Format("laseReading equals nowReading is {0}", bCompare);
                    System.Diagnostics.Debug.WriteLine(outString);
                }

                try
                {
                    if (isHaveLastReading)
                    {
                        if (!_shaking && CheckForShake(_lastReading, reading, ShakeThreshold) && _shakeCount >= 1)
                        {
                            // shaking
                            _shaking = true;
                            _shakeCount = 0;
                            OnShakeDetected();
                        }
                        else if (CheckForShake(_lastReading, reading, ShakeThreshold))
                        {
                            _shakeCount++;
                        }
                        else if (!CheckForShake(_lastReading, reading, 0.2))
                        {
                            _shakeCount = 0;
                            if (_shaking == true)
                            {
                                // shaking stop
                                OnShakeStopped();
                            }
                            _shaking = false;
                        }
                    }
                    else
                    {
                        isHaveLastReading = true;
                    }

                    _lastReading = reading;
                }
                catch
                {
                    /* ignore errors */
                }
            }
        }

        private void OnShakeDetected()
        {
            if (ShakeDetectedHandler != null)
            {
                bool isShaking = true;
                bool isShakeStopped = false;
                ShakeStateEventArgs e = new ShakeStateEventArgs(isShaking, isShakeStopped);
                
                ShakeDetectedHandler(this, e);
            }
        }

        private void OnShakeStopped()
        {
            if (ShakeDetectedHandler != null)
            {
                bool isShaking = false;
                bool isShakeStopped = true;
                ShakeStateEventArgs e = new ShakeStateEventArgs(isShaking, isShakeStopped);

                ShakeDetectedHandler(this, e);
            }
        }

        private static bool CheckForShake(AccelerometerReading last, AccelerometerReading current, double threshold)
        {
            Vector3 lastAcceleration = last.Acceleration;
            Vector3 currentAcceleration = current.Acceleration;

            double deltaX = Math.Abs((lastAcceleration.X - currentAcceleration.X));
            double deltaY = Math.Abs((lastAcceleration.Y - currentAcceleration.Y));
            double deltaZ = Math.Abs((lastAcceleration.Z - currentAcceleration.Z));
            bool bShake = false;

            if (System.Diagnostics.Debugger.IsAttached)
            {
                string outString = String.Format("deltaX is {0}, deltaY is {1}, deltaZ is {2}, ", deltaX, deltaY, deltaZ);
                System.Diagnostics.Debug.WriteLine(outString);
            }

            bShake = (deltaX > threshold && deltaY > threshold) ||
            (deltaX > threshold && deltaZ > threshold) ||
            (deltaY > threshold && deltaZ > threshold);

            return bShake;
        }
    }

    public class ShakeStateEventArgs : EventArgs
    {
        public ShakeStateEventArgs(bool shakingState, bool shakeStoppedState)
        {
            this.shakingState = shakingState;
            this.shakeStoppedState = shakeStoppedState;
        }

        public bool shakingState;
        public bool shakeStoppedState;
    }
}
