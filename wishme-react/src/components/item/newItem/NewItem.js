import { useState } from "react";
import { useHistory } from "react-router";
import { useSelector } from "react-redux";
import { FaCheckCircle } from "react-icons/fa/index";
import Autocomplete from "@material-ui/lab/Autocomplete";
import TextField from "@material-ui/core/TextField";

import classes from "./NewItem.module.css";
import Image from "../../ui/Image";
import useApi from "../../../hooks/use-api";
import Spinner from "../../ui/Spinner";
import BlueBtn from "../../ui/buttons/BlueBtn";

const NewItem = (props) => {
  const history = useHistory();
  const token = useSelector((state) => state.auth.token);

  const [item, setItem] = useState(null);
  const [options, setOptions] = useState();

  const [itemIsSent, setItemIsSent] = useState(false);
  const [dataIsLoading, setDataIsLoading] = useState(false);
  const [comboboxOptions, setComboboxOptions] = useState([]);

  const { isLoading, error, sendRequest } = useApi();

  const curUrl = history.location.pathname;
  const listUrl = curUrl.substring(0, curUrl.lastIndexOf("/"));
  const listId = listUrl.substring(listUrl.lastIndexOf("/") + 1);

  const searchHandler = (term) => {
    if (!term || term.length < 3) return;

    sendRequest(
      {
        url: `items/suggestions/heureka/previews?term=${term}`,
        headers: { Authorization: `Bearer ${token}` },
      },
      (responseData) => {
        setComboboxOptions(responseData.map((i) => i.name));
        setOptions(responseData);
      }
    );
  };

  const getItemHandler = (event, newValue) => {
    if (!newValue) setItem(null);
    if (!newValue) return;

    const item = options.find((v) => v.name === newValue);
    urlHandler(item.url);
  };

  const urlHandler = (url) => {
    const parsedUrl = encodeURIComponent(url);
    setDataIsLoading(true);

    sendRequest(
      {
        url: `items/suggestions/heureka/detail?url=${parsedUrl}`,
        headers: { Authorization: `Bearer ${token}` },
      },
      (responseData) => {
        setItem({
          name: responseData.name,
          description: responseData.description,
          price: responseData.price,
          imageUrl: responseData.imageUrl,
          url: url,
        });
        setDataIsLoading(false);
      }
    );
  };

  const submitHandler = (event) => {
    event.preventDefault();
    setItemIsSent(false);

    sendRequest(
      {
        url: `wishlists/${listId}/items`,
        method: "POST",
        body: JSON.stringify({
          name: item.name,
          description: item.description,
          price: item.price,
          imageUrl: item.imageUrl,
          url: item.url,
        }),
        headers: { Authorization: `Bearer ${token}` },
      },
      () => {
        setItemIsSent(true);
        props.itemAdded();
      }
    );
  };

  return (
    <>
      {!itemIsSent && (
        <div className={classes.section}>
          <div>
            <h2>Zadejte informace o novém předmětu.</h2>
          </div>
          <form className={classes.form} onSubmit={submitHandler}>
            <div className={classes.control}>
              <label htmlFor="searching">Začněte vyhledávat</label>

              <Autocomplete
                onInputChange={(event, newInputValue) => {
                  searchHandler(newInputValue);
                }}
                onChange={(event, newValue) => getItemHandler(event, newValue)}
                style={{
                  fontFamily: "Quicksand, sans-serif",
                  backgroundColor: "#f5f5f5",
                  borderRadius: "4px",
                  border: "1px solid #1b63f19a",
                  fontSize: "1rem",
                  width: "100.6%",
                  textDecoration: "none",
                }}
                clearOnEscape
                options={comboboxOptions}
                renderInput={(params) => (
                  <TextField
                    {...params}
                    InputProps={{
                      ...params.InputProps,
                      endAdornment: (
                        <>
                          {isLoading && !dataIsLoading ? (
                            <Spinner width="1rem" heigh="1rem" />
                          ) : null}
                          {params.InputProps.endAdornment}
                        </>
                      ),
                    }}
                  />
                )}
              />
            </div>

            <p>NEBO</p>

            <div className={classes.control}>
              <label htmlFor="url">Zadejte url předmětu</label>
              <input
                type="url"
                id="url"
                onChange={(e) => urlHandler(e.target.value)}
              />
            </div>

            {(isLoading || dataIsLoading) && (
              <div className={classes.loading}>
                <Spinner />
              </div>
            )}

            {!isLoading && !dataIsLoading && item && (
              <>
                <div className={classes.control}>
                  <label htmlFor="title">Název</label>
                  <input type="text" id="title" defaultValue={item.name} />
                </div>

                <div className={classes.control}>
                  <label htmlFor="price">Cena</label>
                  <input type="text" id="price" defaultValue={item.price} />
                </div>

                <div className={classes.control}>
                  <label htmlFor="description">Popis</label>
                  <textarea
                    type="text"
                    id="description"
                    rows="5"
                    defaultValue={item.description}
                  />
                </div>

                <div className={classes.imageWrap}>
                  <Image
                    src={item.imageUrl}
                    alt={item.name}
                    className={classes.image}
                  />
                </div>
              </>
            )}
            <div className={classes.btn}>
              <BlueBtn width="25%">Přidat předmět</BlueBtn>
            </div>
          </form>
        </div>
      )}
      {itemIsSent && (
        <div className={classes.check}>
          <FaCheckCircle />
        </div>
      )}
    </>
  );
};

export default NewItem;
