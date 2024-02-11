using System;
using System.IO;
using Android.Content;
using Java.IO;
using Xamarin.Forms;
using System.Threading.Tasks;
using AndroidX.Core.Content;

[assembly: Dependency(typeof(SaveAndroid))]

class SaveAndroid : ISave
{
    //Method to save document as a file in Android and view the saved document
     public async Task SaveAndView(string fileName, String contentType, MemoryStream stream)
    {
        string root;
        //Get the root path in android device.
        if (Android.OS.Environment.IsExternalStorageEmulated)
        {
            root = Environment.GetFolderPath(Environment.SpecialFolder.Personal); 
        }
        else
            root = Android.App.Application.Context.FilesDir.Path;

        var directoryName = root + "/FOHQ";
        // удалим старые файлы из папки приложения
        try
        {
            DirectoryInfo di = new DirectoryInfo(directoryName);
            FileInfo[] fileEntries = di.GetFiles();
            foreach (FileInfo f in fileEntries)
            { 
                f.Delete();
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        }
        //Create directory and file 
        Java.IO.File myDir = new Java.IO.File(directoryName);// 
        myDir.Mkdir();

        Java.IO.File file = new Java.IO.File(myDir, fileName);

        //Write the stream into the file
        try
        {
            //The permission failure happens in this line when the target version is set to API Level 27 Nougat
            FileOutputStream outs = new FileOutputStream(file);
            stream.Position = 0;
            outs.Write(stream.ToArray());
            outs.Flush();
            outs.Close();
        }
        catch (Exception e)
        {
            //The permission failure can be seen here if the target version is set to API level 27 Nougat
            System.Console.WriteLine(e);
      
        }
        finally
        {
            stream.Dispose();
        }

        //Invoke the created file for viewing
        if (file.Exists())
        {
            string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
            string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
            try
            {
                var apkURI = FileProvider.GetUriForFile(Android.App.Application.Context, Android.App.Application.Context.ApplicationContext.PackageName + ".fileprovider", file);
                var intent = new Intent(Intent.ActionSend);

                intent.SetFlags(ActivityFlags.GrantReadUriPermission);
                intent.SetType(mimeType);
                intent.PutExtra(Intent.ExtraStream, apkURI);
                intent.PutExtra(Intent.ExtraText, string.Empty);
                intent.PutExtra(Intent.ExtraSubject, "" ?? string.Empty);

                var chooserIntent = Intent.CreateChooser(intent, "" ?? string.Empty);
                chooserIntent.SetFlags(ActivityFlags.ClearTop);
                chooserIntent.SetFlags(ActivityFlags.NewTask);

                Android.App.Application.Context.StartActivity(chooserIntent);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
        }
    }
}
