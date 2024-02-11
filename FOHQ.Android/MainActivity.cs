using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using AndroidX.Core.Content;
using Android;
using System.Globalization;
using System.Threading;

namespace FOHQ.Droid
{
    [Activity(Label = "Мобильное приложение для службы пожаротушения Республики Татарстан", Icon = "@drawable/m_logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            //if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            //{
            //    if (!PermissionGrantedForExternalStorage)
            //    {
            //        RequestPermissions();
            //    }
            //    else
            //    {
            //        RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage }, 0);
            //    }
            //}

        }

        private void RequestPermissions()
        {
            Plugin.Permissions.PermissionsImplementation.Current.RequestPermissionsAsync(
                 new Plugin.Permissions.Abstractions.Permission[]
                 {
             Plugin.Permissions.Abstractions.Permission.Storage,
             Plugin.Permissions.Abstractions.Permission.MediaLibrary
                 });
        } 

        private bool PermissionGrantedForExternalStorage
        {
            get
            {
                Permission permissionResult = ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage);
                if (permissionResult == Permission.Granted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}