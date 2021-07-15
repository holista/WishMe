import Card from "../components/ui/Card";

const IntroductionPage = (props) => {
  return (
    <Card>
      <h1 style={{ textAlign: "center" }}>Jak na WishMe?</h1>
      <h2>Jste organizátorem události?</h2>
      <h3>Vytvořte událost a dejte vědět svým blízkým, co si přejete.</h3>
      <p>
        Nejprve vytvořte událost, vložte jeden či více seznamů přání a do nich
        zadejte, co byste si přáli. Vybírat můžete z celé šíře portálu
        Heureka.cz a nebo zadat jakoukoliv položku ručně. Poté rozešlete svým
        přátelům vygenerovaný odkaz, který najdete na stránce události.
      </p>

      <h2>Jste hostem události?</h2>
      <p>
        Přejděte na odkaz, který Vám poslal organizátor události a vyberte mu
        dárek přesně podle jeho představ. Na stránce si vyberte předmět a
        zamluvte si ho.
      </p>
    </Card>
  );
};

export default IntroductionPage;
