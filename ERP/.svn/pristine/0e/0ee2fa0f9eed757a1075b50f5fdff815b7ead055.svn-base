using CloudinaryDotNet;
using MCC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MCC.Helpers
{
    public class CloudinaryAPI
    {
        public Cloudinary cloudinary()
        {
            var settings = ConfigurationManager.AppSettings;
            CloudinaryDotNet.Account account =
                new CloudinaryDotNet.Account(settings["cloudinary.cloud"], settings["cloudinary.apikey"], settings["cloudinary.apisecret"]);

            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);
            return cloudinary;
        }
        public tw_ImagesSize Upload(HttpPostedFileBase file, string publicId)
        {
            var settings = ConfigurationManager.AppSettings;
            CloudinaryDotNet.Account account =
                new CloudinaryDotNet.Account(settings["cloudinary.cloud"], settings["cloudinary.apikey"], settings["cloudinary.apisecret"]);

            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new CloudinaryDotNet.Actions.FileDescription(file.FileName, file.InputStream),
                PublicId = publicId
            };

            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

            var imagesSize = new tw_ImagesSize()
            {
                thumb = new Helpers.CloudinaryAPI().cloudinary().Api.UrlImgUp.Transform(
                    new Transformation().Width(150).Crop("fill")).BuildUrl(publicId + ".jpg"),
                small = new Helpers.CloudinaryAPI().cloudinary().Api.UrlImgUp.Transform(
                new Transformation().Width(480).Crop("fill")).BuildUrl(publicId + ".jpg"),
                medium = new Helpers.CloudinaryAPI().cloudinary().Api.UrlImgUp.Transform(
                    new Transformation().Width(760).Crop("fill")).BuildUrl(publicId + ".jpg"),
                large = new Helpers.CloudinaryAPI().cloudinary().Api.UrlImgUp.Transform(
                new Transformation().Width(1024).Crop("fill")).BuildUrl(publicId + ".jpg"),
                face = new Helpers.CloudinaryAPI().cloudinary().Api.UrlImgUp.Transform(
                new Transformation().Width(150).Height(150).Crop("thumb").Gravity("face")).BuildUrl(publicId + ".jpg"),
                original = new Helpers.CloudinaryAPI().cloudinary().Api.UrlImgUp.BuildUrl(publicId + ".jpg")
            };
            return imagesSize;
        }

        public void DeleteImage(List<string> publicId)
        {
            var settings = ConfigurationManager.AppSettings;
            CloudinaryDotNet.Account account =
                new CloudinaryDotNet.Account(settings["cloudinary.cloud"], settings["cloudinary.apikey"], settings["cloudinary.apisecret"]);

            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);
            foreach (var item in publicId)
            {
                cloudinary.DeleteResources(item);
            }
        }
    }
}