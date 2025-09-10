import React, { useState } from "react";
import { TextField, Button, Box } from "@mui/material";

type Filters = {
    name: string;
    address: string;
    minPrice: number;
    maxPrice: number;
};

type Props = {
    onFilterChange: (filters: Filters) => void;
};

export const PropertyFilters: React.FC<Props> = ({ onFilterChange }) => {
    const [filters, setFilters] = useState<Filters>({
        name: "",
        address: "",
        minPrice: 0,
        maxPrice: 100000000,
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFilters((prev) => ({
            ...prev,
            [name]: name.includes("Price") ? Number(value) : value,
        }));
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        onFilterChange(filters);
    };

    return (
        <Box component="form" onSubmit={handleSubmit} sx={{ mb: 2 }}>
            <Box
                sx={{
                    display: "flex",
                    flexWrap: "wrap",
                    gap: 2,
                    justifyContent: "center",
                    alignItems: "center",
                }}
            >
                <Box sx={{ flex: { xs: "1 1 100%", sm: "1 1 45%", md: "1 1 22%" } }}>
                    <TextField
                        name="name"
                        label="Nombre"
                        value={filters.name}
                        onChange={handleChange}
                        variant="outlined"
                        size="small"
                        fullWidth
                    />
                </Box>
                <Box sx={{ flex: { xs: "1 1 100%", sm: "1 1 45%", md: "1 1 22%" } }}>
                    <TextField
                        name="address"
                        label="Dirección"
                        value={filters.address}
                        onChange={handleChange}
                        variant="outlined"
                        size="small"
                        fullWidth
                    />
                </Box>
                <Box sx={{ flex: { xs: "1 1 100%", sm: "1 1 45%", md: "1 1 22%" } }}>
                    <TextField
                        name="minPrice"
                        label="Precio mínimo"
                        type="number"
                        value={filters.minPrice}
                        onChange={handleChange}
                        variant="outlined"
                        size="small"
                        fullWidth
                    />
                </Box>
                <Box sx={{ flex: { xs: "1 1 100%", sm: "1 1 45%", md: "1 1 22%" } }}>
                    <TextField
                        name="maxPrice"
                        label="Precio máximo"
                        type="number"
                        value={filters.maxPrice}
                        onChange={handleChange}
                        variant="outlined"
                        size="small"
                        fullWidth
                    />
                </Box>
                <Box sx={{ flex: "1 1 100%" }}>
                    <Button type="submit" variant="contained" color="primary" fullWidth>
                        Filtrar
                    </Button>
                </Box>
            </Box>
        </Box>
    );
}
