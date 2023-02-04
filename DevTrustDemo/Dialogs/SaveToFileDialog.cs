using Microsoft.Win32;
using System;

namespace DevTrustDemo.Dialogs
{
    public class SaveToFileDialog : ISaveToFileDialog
    {
        public string CsvFileSaveDialog()
        {
            string filename = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "csv",
                Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            try {
                if (saveFileDialog.ShowDialog() == true) {
                    if (!saveFileDialog.FileName.EndsWith(".csv")) {
                        filename = saveFileDialog.FileName + ".csv";
                    }
                    else {
                        filename = saveFileDialog.FileName;
                    }
                }

            }
            catch (Exception ex) {

                var ErrMessage = ex.Message;
                throw;
            }

            return filename;
        }

        public string TxtFileSaveDialog()
        {
            string filename = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "txt",
                Filter = "txt Files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            try {
                if (saveFileDialog.ShowDialog() == true) {
                    if (!saveFileDialog.FileName.EndsWith(".txt")) {
                        filename = saveFileDialog.FileName + ".txt";
                    }
                    else {
                        filename = saveFileDialog.FileName;
                    }
                }

            }
            catch (Exception ex) {

                var ErrMessage = ex.Message;
                throw;
            }

            return filename;
        }


    }
}
