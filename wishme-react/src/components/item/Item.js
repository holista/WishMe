import { useEffect, useRef, useState } from "react";
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

  const [item, setItem] = useState(null);

  const [editModeIsActive, setEditModeIsActive] = useState(false);
  const [isRemoving, setIsRemoving] = useState(false);
  const [isUnbooking, setIsUnbooking] = useState(false);

  const nameInputRef = useRef();
  const priceInputRef = useRef();
  const imageUrlInputRef = useRef();
  const descriptionInputRef = useRef();

  const { isLoading, error, sendRequest } = useApi();

  useEffect(() => {
    if (!editModeIsActive) {
      sendRequest(
        {
          url: `items/${id}`,
          headers: { Authorization: `Bearer ${token}` },
        },
        (responseData) => {
          setItem({
            name: responseData.name,
            price: responseData.price,
            imageUrl: responseData.imageUrl,
            claimed: responseData.claimed,
            updatedUtc: responseData.updatedUtc,
            description: responseData.description,
            url: responseData.url,
          });
        }
      );
    }
  }, [token, id, sendRequest, editModeIsActive]);

  const removeItemHandler = () => {
    sendRequest({
      url: `items/${id}`,
      method: "DELETE",
      headers: { Authorization: `Bearer ${token}` },
    });
    history.replace(`/udalost/${evId}`);
  };

  const saveItemHandler = () => {
    const itemData = {
      name: nameInputRef.current.value,
      price: priceInputRef.current.value,
      description: descriptionInputRef.current.value,
      imageUrl: imageUrlInputRef.current.value,
    };

    sendRequest({
      url: `items/${id}`,
      method: "PUT",
      body: JSON.stringify(itemData),
      headers: { Authorization: `Bearer ${token}` },
    });
    setEditModeIsActive(false);
  };

  const bookItemHandler = () => {
    setItem((prevState) => {
      return { ...prevState, claimed: true };
    });
    sendRequest({
      url: `items/${id}/claimed`,
      method: "PUT",
      body: JSON.stringify({ claimed: true }),
      headers: { Authorization: `Bearer ${token}` },
    });
  };

  const unbookItemHandler = () => {
    setItem((prevState) => {
      return { ...prevState, claimed: false };
    });
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
    window.open(item.url, "_blank");
  };

  const changeImageHandler = async (e) => {
    e.preventDefault();
    const thumbnail = imageUrlInputRef.current.value;
    setItem((prevState) => {
      return { ...prevState, thumb: thumbnail };
    });
  };

  return (
    <>
      {isLoading && <Spinner />}
      {!isLoading && item && (
        <Card>
          <EditBar
            arrowBack
            goTo={() => history.goBack()}
            onRemove={() => setIsRemoving(true)}
            editing={!editModeIsActive}
            onEdit={() => setEditModeIsActive(true)}
            onSave={saveItemHandler}
          />

          <div className={classes.name}>
            {!editModeIsActive ? (
              <h1>{item.name}</h1>
            ) : (
              <input
                type="text"
                id="name"
                defaultValue={item.name}
                ref={nameInputRef}
              />
            )}
          </div>

          <div className={classes.details}>
            <section className={classes.section}>
              <div className={classes.control}>
                {!editModeIsActive ? (
                  <h3>{`${item.price} Kč`}</h3>
                ) : (
                  <input
                    type="text"
                    id="price"
                    defaultValue={item.price}
                    ref={priceInputRef}
                  />
                )}
              </div>

              <div className={classes.control}>
                <h3>{item.claimed ? "Zamluveno" : "Volné"}</h3>
              </div>

              <div className={classes.description}>
                {!editModeIsActive ? (
                  <p>{item.description}</p>
                ) : (
                  <textarea
                    type="text"
                    id="description"
                    rows="5"
                    defaultValue={item.description}
                    ref={descriptionInputRef}
                  />
                )}
              </div>
            </section>

            {!editModeIsActive ? (
              <div className={classes.imageWrap}>
                <Image src={item.imageUrl} className={classes.image} />
              </div>
            ) : (
              <div className={classes.controlImg}>
                <div className={classes.thumbWrap}>
                  <Image src={item.thumb || item.imageUrl} />
                </div>
                <input
                  type="url"
                  defaultValue={item.imageUrl}
                  ref={imageUrlInputRef}
                  onChange={changeImageHandler}
                  onClick={(e) => e.target.select()}
                />
              </div>
            )}
          </div>

          {!editModeIsActive && (
            <div className={classes.btns}>
              <div className={classes.btn}>
                {!item.claimed ? (
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
          )}
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
