import { PechRequest } from "../services/peches";
import { Input, Modal } from "antd";
import { useEffect, useState } from "react";

interface Props {
    mode: Mode;
    values: Pech;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: PechRequest) => void;
    handleUpdate: (id: string, request: PechRequest) => void;
}

export enum Mode {
    Create,
    Edit,
}

export const CreateUpdatePech = ({
    mode,
    values,
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate,
}: Props) => {
    const[tPech, setTPech] = useState<number>(0);
    const[height, setHeight] = useState<number>(0);
    const[tNach, setTNach] = useState<number>(0);
    const[kTeplo, setKTeplo] = useState<number>(0);
    const[teplo, setTeplo] = useState<number>(0);
    const[p, setP] = useState<number>(0);
    const[tPov, setTPov] = useState<number>(0);
    const[kTeploOtd, setKTeploOtd] = useState<number>(0);

    useEffect(() => {
        setTPech(values.tPech);
        setTPech(values.height);
        setTPech(values.tNach);
        setTPech(values.kTeplo);
        setTPech(values.teplo);
        setTPech(values.p);
        setTPech(values.tPov);
        setTPech(values.kTeploOtd);
    }, [values]);

    const handleOnOk = async() => {
        const pechRequest = {tPech, height, tNach, kTeplo, teplo, p, tPov, kTeploOtd};

        mode == Mode.Create ? handleCreate(pechRequest) : handleUpdate(values.id, pechRequest)
    }

        return (
            <Modal 
                title={mode === Mode.Create ? "Добавить книгу" : "Редактировать книгу"} 
                open={isModalOpen} 
                cancelText = {"Отмена"}
                onOk={handleOnOk}
                onCancel={handleCancel}>
             <div className = "book_modal">
                <Input
                    value={tPech}
                    onChange={(e) => setTPech(e.target.value)}
                    placeholder="Температура печи"
                />
                <Input
                    value={height}
                    onChange={(e) => setHeight(e.target.value)}
                    placeholder="Высота"
                />
                <Input
                    value={tNach}
                    onChange={(e) => setTNach(e.target.value)}
                    placeholder="Начальная температура"
                />
                <Input
                    value={kTeplo}
                    onChange={(e) => setKTeplo(e.target.value)}
                    placeholder="Коэффициент теплопроводности"
                />
                <Input
                    value={teplo}
                    onChange={(e) => setTeplo(e.target.value)}
                    placeholder="Теплоемкость"
                />
                <Input
                    value={p}
                    onChange={(e) => setP(e.target.value)}
                    placeholder="Плотность"
                />
                <Input
                    value={tPov}
                    onChange={(e) => setTPov(e.target.value)}
                    placeholder="Температура поверхности"
                />
                <Input
                    value={kTeploOtd}
                    onChange={(e) => setKTeploOtd(e.target.value)}
                    placeholder="Коэффициент теплоотдачи"
                />
             </div>
            </Modal>
        );
    };