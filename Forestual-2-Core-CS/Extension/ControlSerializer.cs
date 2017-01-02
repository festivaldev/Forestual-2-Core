using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace F2Core.Extension
{
    public class ControlSerializer
    {
        public static Control CreateControl(string name, string partialName) {
            try {
                Control Control;
                switch (name) {
                case "Label":
                    Control = new Label();
                    break;
                case "TextBox":
                    Control = new TextBox();
                    break;
                case "PictureBox":
                    Control = new PictureBox();
                    break;
                case "ListView":
                    Control = new ListView();
                    break;
                case "ComboBox":
                    Control = new ComboBox();
                    break;
                case "Button":
                    Control = new Button();
                    break;
                case "CheckBox":
                    Control = new CheckBox();
                    break;
                case "MonthCalender":
                    Control = new MonthCalendar();
                    break;
                case "DateTimePicker":
                    Control = new DateTimePicker();
                    break;
                default:
                    var ControlAssembly = Assembly.LoadWithPartialName(partialName);
                    var ControlType = ControlAssembly.GetType(partialName + "." + name);
                    Control = (Control) Activator.CreateInstance(ControlType);
                    break;

                }
                return Control;
            } catch {
                return new Control();
            }
        }

        public static void SetControlProperties(Control control, Hashtable properties) {
            var Properties = TypeDescriptor.GetProperties(control);
            foreach (PropertyDescriptor Property in Properties) {
                if (properties.Contains(Property.Name)) {
                    var Object = properties[Property.Name];
                    try {
                        Property.SetValue(control, Object);
                    } catch  { }
                }
            }
        }

        public static Control CloneControl(Control control) {
            var ExistingControl = new SerializableControl(control);
            var NewControl = CreateControl(ExistingControl.ControlName, ExistingControl.PartialName);
            SetControlProperties(NewControl, ExistingControl.PropertyList);
            return NewControl;
        }

        public static IDataObject SerializeControl(Control control) {
            var ExistingControl = new SerializableControl(control);
            IDataObject DataObject = new DataObject();
            DataObject.SetData(SerializableControl.Format.Name, true, ExistingControl);
            //Clipboard.SetDataObject(DataObject, false);
            return DataObject;
        }

        public static Control DeserializeControl(IDataObject dataObject) { 
            var Control = new Control();
            //var DataObject = Clipboard.GetDataObject();
            var DataObject = dataObject;
            if (DataObject.GetDataPresent(SerializableControl.Format.Name)) {
                var ExistingControl = DataObject.GetData(SerializableControl.Format.Name) as SerializableControl;
                Control = CreateControl(ExistingControl.ControlName, ExistingControl.PartialName);
                SetControlProperties(Control, ExistingControl.PropertyList);
            }
            return Control;
        }
    }

    [Serializable]
    public class SerializableControl
    {
        static SerializableControl() {
            Format = DataFormats.GetFormat(typeof(SerializableControl).FullName);
        }

        public static DataFormats.Format Format { get; }
        public string ControlName { get; set; }
        public string PartialName { get; set; }
        public Hashtable PropertyList { get; } = new Hashtable();

        public SerializableControl(Control control) {
            ControlName = control.GetType().Name;
            PartialName = control.GetType().Namespace;

            var Properties = TypeDescriptor.GetProperties(control);

            foreach (PropertyDescriptor Property in Properties) {
                try {
                    if (Property.PropertyType.IsSerializable)
                        PropertyList.Add(Property.Name, Property.GetValue(control));
                } catch { }
            }
        }
    }
}