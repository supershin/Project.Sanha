using System;
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
                        UploadSignResource(model.Sign, model.ApplicationPath);
                        //model.Sign.ProjectID = model.ProjectID;
                        //model.Sign.UnitCode = model.UnitCode;
                        //model.Sign.FolderName = "UnitEquipment";
                        //SaveSignResourceData(model.Sign);

                    }
                    //jm sign resource
                    if (!string.IsNullOrEmpty(model.SignJM))
                    {
                        UploadSignResource(model.SignJM, model.ApplicationPath);

                        //model.SignJM.ProjectID = model.ProjectID;
                        //model.SignJM.UnitCode = model.UnitCode;
                        //model.SignJM.FolderName = "UnitEquipment";

                        
                        //SaveSignResourceData(model.SignJM);

                    }
                    // save transaction
                    
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
        }

        private Resources UploadSignResource(string model, string appPath)
        {
            Resources resource = new Resources();
            Guid guidId = Guid.NewGuid();
            string filepath = "";
            
            if(model != null)
            {
                var folder = DateTime.Now.ToString("yyyyMM");
                var dirPath = $"Upload/document/{folder}/sign/";
                filepath = dirPath + guidId + ".jpg";

                resource.PhysicalPathServer = appPath;
                resource.ResourceStorageBase64 = model;
                resource.ResourceStoragePath = filepath;
                resource.Directory =Path.Combine(appPath, dirPath);
                ConvertByteToImage(resource);
            }

            return resource;
        }

        private void ConvertByteToImage(Resources item)
        {  
            // Convert the Base64 UUEncoded input into binary output. 
            byte[] binaryData;
            try
            {
                binaryData =
                   System.Convert.FromBase64String(item.ResourceStorageBase64);
            }
            catch (System.ArgumentNullException)
            {
                System.Console.WriteLine("Base 64 string is null.");
                return;
            }
            catch (System.FormatException ex)
            {
                throw ex;
            }

            // Write out the decoded data.
            System.IO.FileStream outFile;
            try
            {
                if (!Directory.Exists(item.Directory))
                {
                    Directory.CreateDirectory(item.Directory);
                }
                var pathFile = string.Format("{0}{1}", item.PhysicalPathServer, item.ResourceStoragePath);
                outFile = new System.IO.FileStream(pathFile,
                                           System.IO.FileMode.Create,
                                           System.IO.FileAccess.Write);
                outFile.Write(binaryData, 0, binaryData.Length);
                outFile.Close();
            }
            catch (System.Exception exp)
            {
                // Error creating stream or writing to it.
                throw exp;
            }
        }
    }
}

