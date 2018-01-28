using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace Convert_Stuff
{
    public class Data_Fragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Unsigned_conversion_layout, container, false);

            EditText inText = view.FindViewById<EditText>(Resource.Id.input);
            TextView outText = view.FindViewById<TextView>(Resource.Id.outputText);
            Spinner inSpinner = view.FindViewById<Spinner>(Resource.Id.inSpinner);
            Spinner outSpinner = view.FindViewById<Spinner>(Resource.Id.outSpinner);
            

            var inAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.data_units, Resource.Layout.my_spinner);
            inSpinner.Adapter = inAdapter;
            inAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            var outAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.data_units, Resource.Layout.my_spinner);
            outSpinner.Adapter = outAdapter;
            outAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            outText.Click += delegate
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this.Context);
                builder.SetTitle("Output");
                builder.SetMessage(Convert.ToString(outText.Text));
                builder.SetPositiveButton("Done", delegate { });
                builder.Show();
            };

            outSpinner.ItemSelected += delegate
            {
                double input;            
                bool validInput = Double.TryParse(inText.Text, out input);
                if (validInput)
                {
                    outText.Text = Convert_units(input, inSpinner.SelectedItemId, outSpinner.SelectedItemId);
                }
                else
                {
                    outText.Text = "";
                }
            };

            var convert = view.FindViewById(Resource.Id.convert_btn);
            convert.Click += delegate
            {
                double input;
                bool validInput = Double.TryParse(inText.Text, out input);
                if (validInput)
                {
                    outText.Text = Convert_units(input, inSpinner.SelectedItemId, outSpinner.SelectedItemId);
                }
                else
                {
                    Toast.MakeText(this.Context, "Enter decimal value", ToastLength.Long).Show();
                }
            };

            return view;
        }

        private string Convert_units(double input, long input_id, long output_id)
        {
            double output = 0;
            double temp = 0;
            switch (input_id)
            {
                case 0:   //Bits
                    temp = input / 8589934592;
                    break;
                case 1:   //Nibbles
                    temp = input / 2147483648;
                    break;
                case 2:   //Bytes
                    temp = input / 1073741824;
                    break;
                case 3:   //Kilobytes
                    temp = input / 1048576;
                    break;
                case 4:   //Megabytes
                    temp = input / 1024;
                    break;
                case 5:   //Gigabytes
                    temp = input;
                    break;
                case 6:   //Terabytes
                    temp = input * 1024;
                    break;
                case 7:   //Petabytes
                    temp = input * 1048576;
                    break;
                case 8:   //Exabytes
                    temp = input * 1073741824;
                    break;
                case 9:   //Zettabytes
                    temp = input * 1099511627776;
                    break;
                case 10:   //Yottabyte
                    temp = input * 1125899906842624;
                    break;
            }       
            switch (output_id)
            {
                case 0:   //Bits
                    output = temp * 8589934592;
                    break;
                case 1:   //Nibbles
                    output = temp * 2147483648;
                    break;
                case 2:   //Bytes
                    output = temp * 1073741824;
                    break;
                case 3:   //Kilobytes
                    output = temp * 1048576;
                    break;
                case 4:   //Megabytes
                    output = temp * 1024;
                    break;
                case 5:   //Gigabytes
                    output = temp;
                    break;
                case 6:   //Terabytes
                    output = temp / 1024;
                    break;
                case 7:   //Petabytes
                    output = temp / 1048576;
                    break;
                case 8:   //Exabytes
                    output = temp / 1073741824;
                    break;
                case 9:   //Zettabytes
                    output = temp / 1099511627776;
                    break;
                case 10:   //Yottabyte
                    output = temp / 1125899906842624;
                    break;
            }     
                return Convert.ToString(Math.Round(output, 10, MidpointRounding.AwayFromZero));
        }
    }
}