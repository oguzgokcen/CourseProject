import React from "react";
import { ClipLoader } from "react-spinners";

const Spinner = ({loading}) => {
    return(
        loading && (
            <div style={styles.spinner}>
                <ClipLoader size={50} color="6200ea"/>
            </div>
        )
    );
}

const styles = {
    spinner: {
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh"
    },
};

export default Spinner;