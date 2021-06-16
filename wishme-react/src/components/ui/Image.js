const Image = (props) => {
  return (
    <div className={props.className}>
      <img src={props.src} alt={props.alt} />
    </div>
  );
};

export default Image;
