using AForge.Video;
using AForge.Video.DirectShow;
using Project;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ZXing;



public class Scanner
{
    private string scannerName;
    private string monikerStringName;
    public VideoCaptureDevice videoCaptureDevice;
    private BarcodeReader barcodeReader;
    private bool isDisposed = false;
    private bool isRunning = false;
    private PAGE page;
    private bool paused = false;


    public enum PAGE
    {
        MAIN_MENUE,
        ADD_ITEM
    }

    public Scanner(string name, string moniker, PAGE p)
    {
        scannerName = name;
        monikerStringName = moniker;
        barcodeReader = new BarcodeReader();
        page = p;
  
    }
    public PAGE Page
    {
        get { return page; }
        set { page = value; }
    }
    public bool Pause
    {
        get { return paused; }
        set { paused = value; }
    }



    public bool Running
    {
        get { return isRunning; }
        set { isRunning = value; }
    }
 
    public string ScannerName
    {
        get { return scannerName; }
        set { scannerName = value; }
    }
    public void startScanner()
    {

        try
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.SignalToStop();
                videoCaptureDevice.WaitForStop();
                Running = false;
            }

            videoCaptureDevice = new VideoCaptureDevice(monikerStringName);
            videoCaptureDevice.NewFrame += NewFrame;
            videoCaptureDevice.Start();
            Running = true;
        }
        catch (Exception ex)
        {
            MainWindow.writeToLogs(ex.ToString());
        }
    }

    private void NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
 
        if (isDisposed)
        {
            MainWindow.writeToLogs("Yes disposedd");
            return;
        }
            


        Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                using (var barcodeBitmap = (Bitmap)System.Drawing.Image.FromStream(ms))
                {
                    var result = barcodeReader.Decode(barcodeBitmap);
                    if (result != null)
                    {
                        MainWindow.writeToLogs("Barcode: " + result.Text);
                        if (page == PAGE.MAIN_MENUE)
                        {
                            Menue.barcodeToAdd = long.Parse(result.Text);
                        }
                        else if (page == PAGE.ADD_ITEM)
                        {
                            AddItemView.barcodeToAdd = long.Parse(result.Text);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MainWindow.writeToLogs(ex.ToString());
        }
        finally
        {
            bitmap.Dispose();
        }


    }
    public void Dispose()
    {
        if (isDisposed)
            return;

        isDisposed = true;

        if (videoCaptureDevice != null)
        {
            videoCaptureDevice.SignalToStop();
            //videoCaptureDevice.WaitForStop();
            while (videoCaptureDevice.IsRunning) ;
            videoCaptureDevice = null;
        }

        barcodeReader = null;
    }
}
