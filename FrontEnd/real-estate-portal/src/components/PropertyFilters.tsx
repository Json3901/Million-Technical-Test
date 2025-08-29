import React, { useState } from "react";
import { TextField, Button, Box, Grid } from "@mui/material";

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
            <Grid
                container
                spacing={2}
                justifyContent="center"
                alignItems="center"
            >
                <Grid item xs={12} sm={6} md={3}>
                    <TextField
                        name="name"
                        label="Nombre"
                        value={filters.name}
                        onChange={handleChange}
                        variant="outlined"
                        size="small"
                        fullWidth
                    />
                </Grid>
                <Grid item xs={12} sm={6} md={3}>
                    <TextField
                        name="address"
                        label="Dirección"
                        value={filters.address}
                        onChange={handleChange}
                        variant="outlined"
                        size="small"
                        fullWidth
                    />
                </Grid>
                <Grid item xs={12} sm={6} md={3}>
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
                </Grid>
                <Grid item xs={12} sm={6} md={3}>
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
                </Grid>
                <Grid item xs={12} md={12}>
                    <Button type="submit" variant="contained" color="primary" fullWidth>
                        Filtrar
                    </Button>
                </Grid>
            </Grid>
        </Box>
    );
}
