import { Link, useHistory, useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import { FaPencilAlt, FaTrashAlt, FaArrowLeft } from "react-icons/fa/index";

import classes from "./Item.module.css";
import Card from "../ui/Card";
import Spinner from "../ui/Spinner";
import Image from "../ui/Image";
import useApi from "../../hooks/use-api";
import { useEffect, useState } from "react";

const Item = (props) => {
  const history = useHistory();
  const token = useSelector((state) => state.auth.token);
  const { evId, listId, id } = useParams();

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

  const editItemHandler = () => {};

  const removeItemHandler = () => {
    sendRequest({
      url: `items/${id}`,
      method: "DELETE",
      headers: { Authorization: `Bearer ${token}` },
    });
    history.replace(`/event/${evId}`);
  };

  const bookItemHandler = () => {
    setClaimed(true);
    sendRequest({
      url: `items/${id}/claimed`,
      method: "PUT",
      body: JSON.stringify({ claimed: true }),
      headers: { Authorization: `Bearer ${token}` },
    });
  };

  const unbookItemHandler = () => {
    setClaimed(false);
    sendRequest({
      url: `items/${id}/claimed`,
      method: "PUT",
      body: JSON.stringify({ claimed: false }),
      headers: { Authorization: `Bearer ${token}` },
    });
  };

  const heurekaHandler = () => {
    window.open(url, "_blank");
  };

  return (
    <>
      {isLoading && <Spinner />}
      {!isLoading && (
        <Card className={classes.item}>
          <div className={classes.edit}>
            <div>
              <button onClick={() => history.goBack()}>
                <FaArrowLeft />
              </button>
            </div>
            <div>
              <button onClick={removeItemHandler}>
                <FaTrashAlt />
              </button>
              <button onClick={editItemHandler}>
                <FaPencilAlt />
              </button>
            </div>
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
            <div className={!claimed ? classes.btn : classes.booked}>
              <button onClick={!claimed ? bookItemHandler : unbookItemHandler}>
                {!claimed ? "Zamluvit" : "Odzamluvit"}
              </button>
            </div>
            <div className={classes.btn}>
              <button onClick={heurekaHandler}>
                Prohlédnout si na Heuréce
              </button>
            </div>
          </div>
        </Card>
      )}
    </>
  );
};

export default Item;
