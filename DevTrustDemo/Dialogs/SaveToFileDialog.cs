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
                Filter = "CSV Files (*.csv)|*.csv"
            };

            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName)) {
                if (!saveFileDialog.FileName.EndsWith(".csv")) {
                    filename = saveFileDialog.FileName + ".csv";
                }
                else {
                    filename = saveFileDialog.FileName;
                }
            }

            return filename;
        }

        public string TxtFileSaveDialog()
        {
            string filename = null;
            XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog
            {
                DefaultExt = "txt",
                Filter = "TXT Files (*.txt)|*.txt"
            };

            saveFileDialog.ShowDialog();

            if (!string.IsNullOrEmpty(saveFileDialog.FileName)) {
                if (!saveFileDialog.FileName.EndsWith(".txt")) {
                    filename = saveFileDialog.FileName + ".txt";
                }
                else {
                    filename = saveFileDialog.FileName;
                }
            }

            return filename;
        }


    }
}
