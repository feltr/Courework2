"use client";

import Button from "antd/es/button/button";
import { Peches } from "../components/Peches";
import { useEffect, useState } from "react";
import { getAllPeches, PechRequest, createPech, updatePech, deletePech } from "../services/peches";
import Title from "antd/es/typography/Title";
import { CreateUpdatePech, Mode } from "../components/CreateUpdatePech";

export default function PechPage() {
    const defaultValues = {
        tPech: 0,
        height: 0,
        tNach: 0,
        kTeplo: 0,
        teplo: 0,
        p: 0,
        tPov: 0,
        kTeploOtd: 0,
        vremNagr: 0,
        t: 0
    } as Pech;

    const [values, setValues] = useState<Pech>(defaultValues);

    const [peches, setPeches] = useState<Pech[]>([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [mode, setMode] = useState(Mode.Create);

    const handleCreatePech = async (request: PechRequest)=>
    {
        await createPech(request);
        closeModal();

        const peches = await getAllPeches();
        setPeches(peches);
    }

    const handleUpdatePech = async (id: string, request: PechRequest) =>
    {
        await updatePech(id, request);
        closeModal();

        const peches = await getAllPeches();
        setPeches(peches);
    }

    const handleDeletePech = async (id: string) => {
        await deletePech(id);
        closeModal();

        const peches = await getAllPeches();
        setPeches(peches);
    };

    const openEditModal = (pech: Pech) => {
        setMode(Mode.Edit);
        setValues(pech);
        setIsModalOpen(true);
    }

    const openModal = () => {
        setIsModalOpen(true);
    }

    const closeModal = () => {
        setValues(defaultValues);
        setIsModalOpen(false);
    }

    useEffect(() => {
        const getPeches = async () => {
            const peches = await getAllPeches();
            setLoading(false);
            setPeches(peches);
        };

        getPeches();
    }, [])

    return(
        <div>
            <Button
                type="primary"
                style={{marginTop:"30px"}}
                size="large"
                onClick={openModal}
                >
                    Добавить книгу
                </Button>
            <CreateUpdatePech 
            mode={mode} 
            values={values} 
            isModalOpen={isModalOpen} 
            handleCreate={handleCreatePech} 
            handleUpdate={handleUpdatePech} 
            handleCancel={closeModal}>

            </CreateUpdatePech>
            {loading ? (<Title>Loading...</Title>) : (
                <Peches peches={peches} 
                handleOpen={openEditModal} 
                handleDelete={handleDeletePech}/>
            )}
        </div>
    )
}