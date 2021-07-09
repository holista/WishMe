import { useEffect, useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import { useSelector } from "react-redux";

import classes from "./Item.module.css";
import Card from "../ui/Card";
import Spinner from "../ui/Spinner";
import Image from "../ui/Image";
import useApi from "../../hooks/use-api";
import EditBar from "../ui/editBar/EditBar";
import Modal from "../ui/Modal";
import GreyBtn from "../ui/buttons/GreyBtn";
import BlueBtn from "../ui/buttons/BlueBtn";

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
  const [isRemoving, setIsRemoving] = useState(false);
  const [isUnbooking, setIsUnbooking] = useState(false);

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
  }, [token, id, sendRequest]);

  const editItemHandler = () => {};

  const removeItemHandler = () => {
    sendRequest({
      url: `items/${id}`,
      method: "DELETE",
      headers: { Authorization: `Bearer ${token}` },
    });
    history.replace(`/udalost/${evId}`);
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
    sendRequest(
      {
        url: `items/${id}/claimed`,
        method: "PUT",
        body: JSON.stringify({ claimed: false }),
        headers: { Authorization: `Bearer ${token}` },
      },
      setIsUnbooking(false)
    );
  };

  const heurekaHandler = () => {
    window.open(url, "_blank");
  };

  return (
    <>
      {isLoading && <Spinner />}
      {!isLoading && (
        <Card className={classes.item}>
          <EditBar
            arrowBack
            goTo={() => history.goBack()}
            onRemove={() => setIsRemoving(true)}
            onEdit={editItemHandler}
          />

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
            <div className={classes.btn}>
              {!claimed ? (
                <BlueBtn onClick={bookItemHandler}>Zamluvit</BlueBtn>
              ) : (
                <GreyBtn onClick={() => setIsUnbooking(true)}>
                  Odzamluvit
                </GreyBtn>
              )}
            </div>
            <div className={classes.btn}>
              <BlueBtn onClick={heurekaHandler}>
                Prohlédnout si na Heuréce
              </BlueBtn>
            </div>
          </div>
        </Card>
      )}
      <Modal
        modalIsOpen={isRemoving}
        onClose={() => setIsRemoving(false)}
        header="Opravdu chcete předmět smazat?"
      >
        <div className={classes.btnModal}>
          <GreyBtn onClick={removeItemHandler} width="50%">
            Smazat
          </GreyBtn>
        </div>
      </Modal>
      <Modal
        modalIsOpen={isUnbooking}
        onClose={() => setIsUnbooking(false)}
        header="Opravdu chcete předmět odbookovat?"
      >
        <p>
          Pokud jste předmět nezamluvili vy, smažete tak rezervaci jinému
          uživateli.
        </p>
        <div className={classes.btnModal}>
          <GreyBtn onClick={unbookItemHandler} width="50%">
            Odbookovat
          </GreyBtn>
        </div>
      </Modal>
    </>
  );
};

export default Item;
