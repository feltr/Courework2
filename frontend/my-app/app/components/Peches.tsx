import Card from "antd/es/card/Card"
import { CardTitle } from "./Cardtitle"
import Button from "antd/es/button/button";

interface Props {
    peches: Pech[]
    handleDelete: (id: string) => void;
    handleOpen:(pech: Pech) => void;
}

export const Peches = ({peches, handleDelete, handleOpen}: Props) => {
    return(
        <div className="cards">
            {peches.map((pech : Pech) => (
                <Card 
                    key={pech.id} 
                    title = {<p>Время нагрева = {pech.vremNagr} Температура = {pech.t}</p>}
                    bordered={false}
                >
                    <p>{pech.id}</p>
                    <div className = "card_buttons">
                        <Button 
                            onClick={() =>handleOpen(pech)}
                            style={{ flex: 1 }}
                        >
                            Редактировать
                        </Button>
                        <Button 
                            onClick={() =>handleDelete(pech.id)}
                            danger
                            style={{ flex: 1 }}
                        >
                            Удалить
                        </Button>
                    </div>
                </Card>
            ))}
        </div>
    );
};