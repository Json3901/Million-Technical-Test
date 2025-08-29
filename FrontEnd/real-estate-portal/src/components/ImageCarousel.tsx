import React, { useState } from "react";
import { Box, IconButton } from "@mui/material";
import ArrowBackIosNewIcon from "@mui/icons-material/ArrowBackIosNew";
import ArrowForwardIosIcon from "@mui/icons-material/ArrowForwardIos";

type Props = {
    images: { urlFile: string }[];
};

export const ImageCarousel: React.FC<Props> = ({ images }) => {
    const [index, setIndex] = useState(0);

    if (!images || images.length === 0) return null;

    const handlePrev = () => setIndex((prev) => (prev === 0 ? images.length - 1 : prev - 1));
    const handleNext = () => setIndex((prev) => (prev === images.length - 1 ? 0 : prev + 1));

    return (
        <Box sx={{
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
            width: "100%",
            maxWidth: 400,
            mx: "auto",
            position: "relative"
        }}>
            <IconButton onClick={handlePrev} sx={{ position: "absolute", left: 0 }}>
                <ArrowBackIosNewIcon />
            </IconButton>
            <Box sx={{ width: "100%", textAlign: "center" }}>
                <img
                    src={images[index].urlFile}
                    alt={`Imagen ${index + 1}`}
                    style={{ width: "100%", maxHeight: 250, objectFit: "cover", borderRadius: 8 }}
                />
            </Box>
            <IconButton onClick={handleNext} sx={{ position: "absolute", right: 0 }}>
                <ArrowForwardIosIcon />
            </IconButton>
        </Box>
    );
};
