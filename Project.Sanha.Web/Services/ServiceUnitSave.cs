using System;
using Humanizer.Localisation;
using System.Transactions;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Services
{
	public class ServiceUnitSave : IServiceUnitSave
	{
		public ServiceUnitSave()
		{
		}

        public void SaveUnitEquipmentSign(CreateTransactionModel model)
        {
            TransactionOptions option = new TransactionOptions();
            option.Timeout = new TimeSpan(1, 0, 0);
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, option))
            {
                try
                {
                    //customer sign resource
                    if (!string.IsNullOrEmpty(model.Sign))
                    {
                        UploadSignResource(model.Sign);

                        //model.Sign.ProjectID = model.ProjectID;
                        //model.Sign.UnitCode = model.UnitCode;
                        //model.Sign.FolderName = "UnitEquipment";
                        //SaveSignResourceData(model.Sign);

                    }
                    //jm sign resource
                    if (!string.IsNullOrEmpty(model.SignJM))
                    {
                        UploadSignResource(model.SignJM);

                        //model.SignJM.ProjectID = model.ProjectID;
                        //model.SignJM.UnitCode = model.UnitCode;
                        //model.SignJM.FolderName = "UnitEquipment";
                        //SaveSignResourceData(model.SignJM);

                    }
                    //saveUnitEquipmentDocumentNo(model);
                    //saveUnitEquipmentDocument(model);
                    //SaveUnitEquipmentSignData(model);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scope.Dispose();
                }
            }
            //GenerateUnitEquipment(model);
            //SaveFileStorage($"{model.ApplicationPath}{model.DocumentPath}", model.ContentByte);
        }

        private void UploadSignResource(string model)
        {
            //Resources resource = new Resources();
            //resource.PhysicalPathServer = model.PhysicalPathServer;
            //resource.ResourceStorageBase64 = model.StorageBase64;
            //resource.ResourceStoragePath = model.FilePath;
            //resource.Directory = model.Directory;
            //ConvertByteToImage(resource);
        }

        //private void ConvertByteToImage(Resources item)
        //{
        //    //byte[] imageBytes = null;

        //    //// Convert Base64 String to byte[]
        //    //imageBytes = Convert.FromBase64String(item.ResourceStorageBase64);
        //    //MemoryStream ms = new MemoryStream(imageBytes, 0,
        //    //imageBytes.Length);

        //    //using (FileStream fs = File.Create(string.Format("{0}{1}", item.PhysicalPathServer, item.ResourceStoragePath))) //set
        //    //{
        //    //    fs.Write(imageBytes, 0, (int)imageBytes.Length);
        //    //}

        //    // Convert the Base64 UUEncoded input into binary output. 
        //    byte[] binaryData;
        //    try
        //    {
        //        binaryData =
        //           System.Convert.FromBase64String(item.ResourceStorageBase64);
        //    }
        //    catch (System.ArgumentNullException)
        //    {
        //        System.Console.WriteLine("Base 64 string is null.");
        //        return;
        //    }
        //    catch (System.FormatException ex)
        //    {
        //        throw ex;
        //    }

        //    // Write out the decoded data.
        //    System.IO.FileStream outFile;
        //    try
        //    {
        //        if (!Directory.Exists(item.Directory))
        //        {
        //            Directory.CreateDirectory(item.Directory);
        //        }
        //        var pathFile = string.Format("{0}{1}", item.PhysicalPathServer, item.ResourceStoragePath);
        //        outFile = new System.IO.FileStream(pathFile,
        //                                   System.IO.FileMode.Create,
        //                                   System.IO.FileAccess.Write);
        //        outFile.Write(binaryData, 0, binaryData.Length);
        //        outFile.Close();
        //    }
        //    catch (System.Exception exp)
        //    {
        //        // Error creating stream or writing to it.
        //        throw exp;
        //    }
        //}
    }
}

