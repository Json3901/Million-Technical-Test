import React from "react";
import { Box, Typography } from "@mui/material";

export const Header: React.FC = () => (
    <Box
        sx={{
            textAlign: "center",
            py: 3,
            bgcolor: "primary.main",
            color: "primary.contrastText",
            mb: 2,
        }}
    >
        <Typography variant="h4" component="h1" fontWeight="bold">
            Real Estate Portal
        </Typography>
        <Typography variant="subtitle1">
            Encuentra y gestiona propiedades fácilmente.
        </Typography>
    </Box>
);
