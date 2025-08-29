import React, {useState} from "react";
import {
    Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, TablePagination, Box, Button
} from "@mui/material";
import {PropertyDetailModal} from "./PropertyDetailModal";

type Property = {
    id: string;
    name: string;
    address: string;
    price: number;
    ownerName?: string;
    internalCode?: string;
    year?: string;
};

type Props = {
    items: Property[];
    count: number;
    page: number;
    pageSize: number;
    onPageChange: (event: unknown, newPage: number) => void;
    onPageSizeChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
};

export const PropertyTable: React.FC<Props> = ({
                                                   items,
                                                   count,
                                                   page,
                                                   pageSize,
                                                   onPageChange,
                                                   onPageSizeChange,
                                               }) => {
    const [modalOpen, setModalOpen] = useState(false);
    const [selectedId, setSelectedId] = useState<string | null>(null);

    const handleView = (id: string) => {
        setSelectedId(id);
        setModalOpen(true);
    };

    const handleClose = () => {
        setModalOpen(false);
        setSelectedId(null);
    };

    const minHeight = 300;
    const maxHeight = Math.min(600, 60 * items.length + 100);

    return (
        <Box display="flex" justifyContent="center">
            <Paper sx={{width: "100%", maxWidth: 900, minWidth: 350}}>
                <TableContainer sx={{minHeight, maxHeight}}>
                    <Table>
                        <TableHead>
                            <TableRow sx={{bgcolor: "grey.400"}}>
                                <TableCell>Nombre</TableCell>
                                <TableCell>Dirección</TableCell>
                                <TableCell>Precio</TableCell>
                                <TableCell>Propietario</TableCell>
                                <TableCell>Código Interno</TableCell>
                                <TableCell>Año</TableCell>
                                <TableCell>Detalle</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {items.map((property, idx) => (
                                <TableRow
                                    key={property.id}
                                    sx={{
                                        "&:hover": {
                                            backgroundColor: "grey.100",
                                        },
                                    }}
                                >
                                    <TableCell>{property.name}</TableCell>
                                    <TableCell>{property.address}</TableCell>
                                    <TableCell>{property.price}</TableCell>
                                    <TableCell>{property.ownerName}</TableCell>
                                    <TableCell>{property.internalCode}</TableCell>
                                    <TableCell>{property.year}</TableCell>
                                    <TableCell>
                                        <Button
                                            variant="contained"
                                            color="primary"
                                            onClick={() => handleView(property.id)}
                                        >
                                            Ver
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
                <TablePagination
                    component="div"
                    count={count}
                    page={page - 1}
                    onPageChange={onPageChange}
                    rowsPerPage={pageSize}
                    onRowsPerPageChange={onPageSizeChange}
                    rowsPerPageOptions={[5, 10, 25, 50]}
                />
            </Paper>
            <PropertyDetailModal
                propertyId={selectedId}
                open={modalOpen}
                onClose={handleClose}
            />
        </Box>
    );
}
