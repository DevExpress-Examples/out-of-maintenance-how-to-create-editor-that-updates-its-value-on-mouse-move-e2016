using System;
using System.Windows.Forms;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;

namespace DXSample {
    public class MySpinEdit :SpinEdit {
        static MySpinEdit() { RepositoryItemMySpinEdit.RegisterMySpinEdit(); }
        public MySpinEdit() : base() { 
            fValueUpdateTimer = new Timer();
            fValueUpdateTimer.Interval = 200;
            ValueUpdateTimer.Tick += delegate(object sender, EventArgs e) { Value += ValueUpdateStep; };
            ValueUpdateStep = 0m;
        }



        public override string EditorTypeName { get { return RepositoryItemMySpinEdit.MySpinEditName; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMySpinEdit Properties { 
            get { return (RepositoryItemMySpinEdit)base.Properties; } 
        }

        private Timer fValueUpdateTimer;
        private Timer ValueUpdateTimer { get { return fValueUpdateTimer; } }
        private decimal fValueUpdateStep;
        private decimal ValueUpdateStep { get { return fValueUpdateStep; } set { fValueUpdateStep = value; } }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left) {
                ValueUpdateStep = e.Location.Y > Height / 2 ? -1m : 1m;
                if (ViewInfo.CalcHitInfo(e.Location).HitTest == EditHitTest.Button) 
                    ValueUpdateTimer.Start();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            if (ValueUpdateTimer.Enabled) ValueUpdateTimer.Stop();
        }
    }

    [UserRepositoryItem("RegisterMySpinEdit")]
    public class RepositoryItemMySpinEdit :RepositoryItemSpinEdit {
        static RepositoryItemMySpinEdit() { RegisterMySpinEdit(); }
        public RepositoryItemMySpinEdit() : base() { }

        internal const string MySpinEditName = "MySpinEdit";

        public override string  EditorTypeName { get { return MySpinEditName; } }

        public static void RegisterMySpinEdit() {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(MySpinEditName, typeof(MySpinEdit),
                typeof(RepositoryItemMySpinEdit), typeof(BaseSpinEditViewInfo), new ButtonEditPainter(), 
                true));
        }
    }
}