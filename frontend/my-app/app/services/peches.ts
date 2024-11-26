import { headers } from "next/headers";

export interface PechRequest{
    tPech: number;
    height: number;
    tNach: number;
    kTeplo: number;
    teplo: number;
    p: number;
    tPov: number;
    kTeploOtd: number
}

export const getAllPeches = async () => {
    const response = await fetch("https://localhost:7057/controller");

    return response.json();
}

export const createPech = async (pechRequest: PechRequest) => {
    await fetch("https://localhost:7057/controller", {
    method: "POST",
    headers:{
        "content-type":"application/json",
    },
    body: JSON.stringify(pechRequest),
    });
};

export const updatePech = async (id: string, pechRequest: PechRequest) => {
    await fetch(`https://localhost:7057/controller/${id}`, {
    method: "PUT",
    headers:{
        "content-type":"application/json",
    },
    body: JSON.stringify(pechRequest),
    });
};

export const deletePech = async (id: string) => {
    await fetch(`https://localhost:7057/controller/${id}`, {
    method: "DELETE",
    });
};