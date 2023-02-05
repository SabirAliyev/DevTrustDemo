using DevExpress.XtraEditors;
using System;

namespace DevTrustDemo.Dialogs
{
    public class SaveToFileDialog : ISaveToFileDialog
    {
        public string CsvFileSaveDialog()
        {
            string filename = null;
            XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog
            {
                DefaultExt = "csv",
                Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            saveFileDialog.ShowDialog();

            if (!saveFileDialog.FileName.EndsWith(".csv")) {
                filename = saveFileDialog.FileName + ".csv";
            }
            else {
                filename = saveFileDialog.FileName;
            }

            return filename;
        }

        public string TxtFileSaveDialog()
        {
            string filename = null;
            XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog
            {
                DefaultExt = "txt",
                Filter = "txt Files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            saveFileDialog.ShowDialog();

            if (!saveFileDialog.FileName.EndsWith(".txt")) {
                filename = saveFileDialog.FileName + ".txt";
            }
            else {
                filename = saveFileDialog.FileName;
            }

            return filename;
        }


    }
}
