interface Props{
    vremNagr: number;
    t: number;
}

export const CardTitle = ({vremNagr, t}: Props) => {
    return(
        <div style={{
            display: "flex",
            flexDirection:"row",
            alignItems:"center",
            justifyContent:"space-between",
        }}>
            <p className="card_vremNagr">(vremNagr)</p>
            <p className="card_t">(t)</p>
        </div>
    )
}