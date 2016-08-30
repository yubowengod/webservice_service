using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebApplication1
{
    public class Image
    {
        public String uploadImage(String filename, String image)
        {
            FileOutputStream fos = null;
            try
            {
                String toDir = "D:\\work\\image";   //存储路径    
                byte[] buffer = new BASE64Decoder().decodeBuffer(image);   //对android传过来的图片字符串进行解码     
                File destDir = new File(toDir);
                if (!destDir.exists())
                {
                    destDir.mkdir();
                }
                fos = new FileOutputStream(new File(destDir, filename));   //保存图片    
                fos.write(buffer);
                fos.flush();
                fos.close();
                return "上传图片成功!" + "图片路径为：" + toDir;
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
            return "上传图片失败！";
        }  
    }
}