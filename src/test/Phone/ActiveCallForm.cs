using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using JulMar.Atapi;

namespace Phone
{
    public partial class ActiveCallForm : Form
    {
        private ITapiCall _call;

        public ActiveCallForm(ITapiCall call)
        {
            var line = call.Line;

            _call = call;
            line.CallInfoChanged += OnCallInfoChange;
            line.CallStateChanged += OnCallStateChange;

            InitializeComponent();
        }

        private void OnCallInfoChange(object sender, CallInfoChangeEventArgs e)
        {
            if (e.Call == _call)
            {
                System.Threading.ThreadStart ts = ChangeButtons;
                BeginInvoke(ts, null);
            }
        }

        private void OnCallStateChange(object sender, CallStateEventArgs e)
        {
            if (e.Call == _call)
            {
                System.Threading.ThreadStart ts = ChangeButtons;
                BeginInvoke(ts, null);
            }
        }

        private void ActiveCallForm_Load(object sender, EventArgs e)
        {
            PropertyExpander pe = new PropertyExpander(_call);
            pe.IgnoredProperties = new string[] { "Features" };
            propertyGrid1.SelectedObject = pe;
            this.Text = string.Format("Tapi Call {0}", _call.GetHashCode());

            ChangeButtons();
        }

        private void ChangeButtons()
        {
            propertyGrid1.Refresh();

            btnAccept.Enabled = (_call.Features.CanAccept);
            btnAnswer.Enabled = (_call.Features.CanAnswer);
            btnDrop.Enabled = (_call.Features.CanDrop);
            btnHold.Enabled = (_call.Features.CanHold);
            btnUnhold.Enabled = (_call.Features.CanUnhold);
            btnSwapHold.Enabled = (_call.Features.CanSwapHold);
            btnPark.Enabled = (_call.Features.CanPark);
            btnDial.Enabled = (_call.Features.CanDial);
            btnSetupTransfer.Enabled = (_call.Features.CanSetupTransfer);
            btnCompleteTransfer.Enabled = (_call.Features.CanCompleteTransfer);
        }

        private void ActiveCallForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _call.Drop();
            }
            catch
            {
            }

            _call.Line.CallInfoChanged -= OnCallInfoChange;
            _call.Line.CallStateChanged -= OnCallStateChange;

            try
            {
                _call.Dispose();
            }
            catch
            {
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            _call.Accept();
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            _call.Drop();
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            _call.Answer();
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            _call.Hold();
        }

        private void btnUnhold_Click(object sender, EventArgs e)
        {
            _call.Unhold();
        }

        private void btnSwapHold_Click(object sender, EventArgs e)
        {
            SelectCallForm scf = new SelectCallForm(_call.Line);
            if (scf.ShowDialog() == DialogResult.OK)
            {
                var otherCall = scf.SelectedCall;
                if (otherCall != null && otherCall != _call)
                {
                    _call.SwapHold(otherCall);
                }
            }
        }

        private void btnPark_Click(object sender, EventArgs e)
        {
            GetDialableNumberForm gdf = new GetDialableNumberForm("Park");
            if (gdf.ShowDialog() == DialogResult.OK)
            {
                if (gdf.Number.Length > 0)
                {
                    _call.Park(gdf.Number);
                }
                else
                    _call.Park();
            }
        }

        private void btnBlindTransfer_Click(object sender, EventArgs e)
        {
            GetDialableNumberForm gdf = new GetDialableNumberForm("Transfer");
            if (gdf.ShowDialog() == DialogResult.OK)
            {
                if (gdf.Number.Length > 0)
                {
                    _call.BlindTransfer(gdf.Number, 0);
                }
            }
        }

        private void btnSetupTransfer_Click(object sender, EventArgs e)
        {
            TapiCall newCall = _call.SetupTransfer(null);
            if (newCall != null)
            {
                ActiveCallForm acf = new ActiveCallForm(newCall);
                acf.Show();
            }
        }

        private void btnCompleteTransfer_Click(object sender, EventArgs e)
        {
            SelectCallForm scf = new SelectCallForm(_call.Line);
            if (scf.ShowDialog() == DialogResult.OK)
            {
                TapiCall otherCall = scf.SelectedCall;
                if (otherCall != null && otherCall != _call)
                {
                    _call.CompleteTransfer(otherCall);
                }
            }
        }

        private void btnDial_Click(object sender, EventArgs e)
        {
            GetDialableNumberForm gdf = new GetDialableNumberForm("Dial");
            if (gdf.ShowDialog() == DialogResult.OK)
            {
                if (gdf.Number.Length > 0)
                {
                    _call.Dial(gdf.Number, 0);
                }
            }
        }
    }

    public class PropertyExpander : ICustomTypeDescriptor
    {
        List<string> _ignoreProps;
        object _ob;

        public PropertyExpander(object o)
        {
            _ob = o;
        }

        public string[] IgnoredProperties
        {
            get { return _ignoreProps.ToArray();  }
            set { _ignoreProps = new List<string>(value); }
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(_ob, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(_ob, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(_ob, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(_ob, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(_ob, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(_ob, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(_ob, _ob.GetType());
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(_ob, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(_ob, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            // Create a new collection object PropertyDescriptorCollection
            PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

            // Iterate the list of properties
            foreach (PropertyInfo pi in _ob.GetType().GetProperties())
            {
                if (_ignoreProps.Contains(pi.Name) == false)
                {
                    pds.Add(new InternalPropertyDescriptor(pi));
                }
            }
            
            return pds;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return _ob;
        }
    };

    public class InternalPropertyDescriptor : PropertyDescriptor
    {
        private PropertyInfo _prop;

        public InternalPropertyDescriptor(PropertyInfo prop)
            : base(prop.Name,
            (Attribute[])prop.GetCustomAttributes(typeof(Attribute), true))
        {
            _prop = prop;
        }

        public override bool Equals(object obj)
        {
            InternalPropertyDescriptor other = obj as InternalPropertyDescriptor;
            return other != null && other._prop.Equals(_prop);
        }

        public override int GetHashCode() { return _prop.GetHashCode(); }
        public override bool IsReadOnly { get { return false; } }
        public override void ResetValue(object component) { }
        public override bool CanResetValue(object component) { return false; }
        public override bool ShouldSerializeValue(object component) {return true; }
        public override Type ComponentType { get { return _prop.DeclaringType; } }
        public override Type PropertyType { get { return _prop.PropertyType; } }
        public override object GetValue(object component) { return _prop.GetValue(component, null); }
        public override void SetValue(object component, object value)
        {
            try
            {
                _prop.SetValue(component, value, null);
                OnValueChanged(component, EventArgs.Empty);
            }
            catch
            {
            }
        }
    }
}