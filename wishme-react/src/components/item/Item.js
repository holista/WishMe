import { Link, useHistory, useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import { FaPencilAlt } from "react-icons/fa/index";

import classes from "./Item.module.css";
import Card from "../ui/Card";
import Spinner from "../ui/Spinner";
import Image from "../ui/Image";
import useApi from "../../hooks/use-api";
import { useEffect, useState } from "react";

const Item = (props) => {
  const history = useHistory();
  const token = useSelector((state) => state.auth.token);
  const { id } = useParams();

  const [name, setName] = useState(null);
  const [price, setPrice] = useState(null);
  const [imageUrl, setImageUrl] = useState(null);
  const [claimed, setClaimed] = useState(false);
  const [updatedUtc, setUpdatedUtc] = useState(null);
  const [description, setDescription] = useState(null);
  const [url, setUrl] = useState(null);

  const { isLoading, error, sendRequest } = useApi();

  useEffect(() => {
    sendRequest(
      {
        url: `items/${id}`,
        headers: { Authorization: `Bearer ${token}` },
      },
      (responseData) => {
        setName(responseData.name);
        setPrice(responseData.price);
        setImageUrl(responseData.imageUrl);
        setClaimed(responseData.claimed);
        setUpdatedUtc(responseData.updatedUtc);
        setDescription(responseData.description);
        setUrl(responseData.url);
      }
    );
  }, [token, sendRequest]);

  const editHandler = () => {};

  const bookHandler = () => {};

  const heurekaHandler = () => {
    window.open(url, "_blank");
  };

  return (
    <>
      {isLoading && <Spinner />}
      {!isLoading && (
        <Card className={classes.item}>
          <div className={classes.edit}>
            <button onClick={editHandler}>
              <FaPencilAlt />
            </button>
          </div>

          <div className={classes.name}>
            <h1>{name}</h1>
          </div>

          <div className={classes.details}>
            <section className={classes.section}>
              <div className={classes.control}>
                {
                  //<span className={classes.icon}>{calendarIcon}</span>
                }
                <h3>{price}</h3>
              </div>
              <div className={classes.control}>
                {
                  //<span className={classes.icon}>{clockIcon}</span>
                }
                <h3>{claimed ? "Zamluveno" : "Volné"}</h3>
              </div>
              <div>
                <p>{description}</p>
              </div>
            </section>

            <div className={classes.imageWrap}>
              <Image src={imageUrl} className={classes.image} />
            </div>
          </div>

          <div className={classes.btns}>
            <button onClick={bookHandler}>Zamluvit</button>

            <button onClick={heurekaHandler}>Prohlédnout si na Heuréce</button>
          </div>
        </Card>
      )}
    </>
  );
};

export default Item;
