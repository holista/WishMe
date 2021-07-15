import { useState } from "react";

const useImage = () => {
  const [imgIsLoading, setImgIsLoading] = useState(false);

  const toBase64 = async (file) => {
    setImgIsLoading(true);
    try {
      const imageStr = await toBase64Convertor(file);
      const index = imageStr.indexOf("base64,");
      return imageStr.substring(index + 7);
    } catch (e) {
      return "";
    } finally {
      setImgIsLoading(false);
    }
  };

  const toBase64Convertor = (file) => {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result.toString());
      reader.onerror = (error) => reject(error);
    });
  };

  return { imgIsLoading, toBase64 };
};

export default useImage;
