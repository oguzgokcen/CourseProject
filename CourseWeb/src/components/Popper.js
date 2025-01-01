import React, { useRef, RefObject, useEffect, useState } from "react";

const Popper = () => {

    const divEl1 = useRef();
    const divEl2 = useRef();
    const [isOver, setIsOver] = useState(false);

    useEffect(() => {
        const { current } = divEl1;
        if (current) {
            current.addEventListener("mouseover", () => setIsOver(true));
            current.removeEventListener("mouseover", () => setIsOver(true));
        }
    }, [divEl1]);

    useEffect(() => {
        const { current } = divEl2;
        if (current) {
            current.addEventListener("mouseout", () => setIsOver(false));
            current.removeEventListener("mouseout", () => setIsOver(false));
        }
    }, [divEl2]);

    return (
        <div >
            <div ref={divEl1}>
                Popper
            </div>
            <div style={{ display: isOver ? "block" : "none" }} ref={divEl2}>
                Show/NotShow
            </div>
        </div>
    )
}
export default Popper;