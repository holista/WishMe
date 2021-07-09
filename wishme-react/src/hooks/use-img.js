const useImage = () => {
  const toBase64 = async (file) => {
    try {
      const imageStr = await toBase64Convertor(file);
      const index = imageStr.indexOf("base64,");
      return imageStr.substring(index + 7);
    } catch (e) {
      return "";
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

  return { toBase64 };
};

export default useImage;
