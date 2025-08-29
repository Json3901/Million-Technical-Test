import React, { useState } from "react";
import { Box, Container } from "@mui/material";
import { Header } from "../components/Header";
import { PropertyFilters } from "../components/PropertyFilters";
import { PropertyTable } from "../components/PropertyTable";
import { useProperties } from "../hooks/useProperties";

export default function HomePage() {
    const [filters, setFilters] = useState({
        name: "",
        address: "",
        minPrice: 0,
        maxPrice: 100000000,
    });
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const { items, count, pageNumber, pageSize: currentPageSize } = useProperties(filters, page, pageSize);

    const handlePageChange = (_event: unknown, newPage: number) => {
        setPage(newPage + 1);
    };

    const handlePageSizeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPageSize(parseInt(event.target.value, 10));
        setPage(1);
    };

    return (
        <Container maxWidth="md">
            <Header />
            <Box display="flex" justifyContent="center" mb={3}>
                <PropertyFilters onFilterChange={setFilters} />
            </Box>
            <PropertyTable
                items={items}
                count={count}
                page={pageNumber}
                pageSize={currentPageSize}
                onPageChange={handlePageChange}
                onPageSizeChange={handlePageSizeChange}
            />
        </Container>
    );
}
