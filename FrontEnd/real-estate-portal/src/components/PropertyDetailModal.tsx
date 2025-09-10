import React, { useEffect, useState } from "react";
import { Modal, Box, Typography, CircularProgress, Divider } from "@mui/material";
import { getPropertyDetail } from "../services/propertyService";
import { ImageCarousel } from "./ImageCarousel";
import { PropertyDetail, PropertyTrace } from "../types/PropertyDetail";

type Props = {
    propertyId: string | null;
    open: boolean;
    onClose: () => void;
};

export const PropertyDetailModal: React.FC<Props> = ({ propertyId, open, onClose }) => {
    const [loading, setLoading] = useState(false);
    const [detail, setDetail] = useState<PropertyDetail | null>(null);

    useEffect(() => {
        if (open && propertyId) {
            setLoading(true);
            getPropertyDetail(propertyId)
                .then((data) => setDetail(data))
                .catch(() => setDetail(null))
                .finally(() => setLoading(false));
        }
    }, [open, propertyId]);

    return (
        <Modal open={open} onClose={onClose}>
            <Box sx={{
                bgcolor: "background.paper",
                p: { xs: 2, sm: 4 },
                maxWidth: 700,
                mx: "auto",
                mt: { xs: "5vh", sm: "10vh" },
                borderRadius: 2,
                boxShadow: 24,
                width: { xs: "95%", sm: "80%" }
            }}>
                {loading ? (
                    <Box sx={{ display: "flex", justifyContent: "center", alignItems: "center", minHeight: 200 }}>
                        <CircularProgress />
                    </Box>
                ) : detail ? (
                    <Box sx={{
                        display: "flex",
                        flexWrap: "wrap",
                        gap: 3,
                        justifyContent: "center",
                        alignItems: "center"
                    }}>
                        <Box sx={{
                            flex: { xs: "1 1 100%", md: "1 1 50%" },
                            display: "flex",
                            justifyContent: { xs: "center", md: "flex-start" }
                        }}>
                            <ImageCarousel images={detail.images || []} />

                        </Box>
                        <Box sx={{
                            flex: { xs: "1 1 100%", md: "1 1 50%" },
                            textAlign: { xs: "center", md: "left" }
                        }}>
                            <Typography variant="h6" gutterBottom>{detail.name}</Typography>
                            <Typography variant="body2" color="text.secondary" gutterBottom>
                                <b>Dirección:</b> {detail.address}
                            </Typography>
                            <Typography variant="body2" color="text.secondary" gutterBottom>
                                <b>Precio:</b> ${detail.price}
                            </Typography>
                            <Typography variant="body2" color="text.secondary" gutterBottom>
                                <b>Propietario:</b> {detail.owner}
                            </Typography>
                            <Typography variant="body2" color="text.secondary" gutterBottom>
                                <b>Año:</b> {detail.year}
                            </Typography>
                            <Typography variant="body2" color="text.secondary" gutterBottom>
                                <b>Código Interno:</b> {detail.internalCode}
                            </Typography>
                        </Box>
                        <Box sx={{ flex: "1 1 100%" }}>
                            <Divider sx={{ my: 2 }} />
                            <Typography variant="subtitle1" gutterBottom textAlign={{ xs: "center", md: "left" }}>
                                Historial de ventas:
                            </Typography>
                            <Box sx={{
                                display: "flex",
                                flexWrap: "wrap",
                                gap: 2,
                                justifyContent: "center"
                            }}>
                                {detail.traces?.map((trace: PropertyTrace, idx: number) => (
                                    <Box key={idx} sx={{
                                        flex: { xs: "1 1 100%", sm: "1 1 45%", md: "1 1 30%" },
                                        bgcolor: "grey.100",
                                        p: 2,
                                        borderRadius: 2,
                                        mb: 1,
                                        boxShadow: 1,
                                        textAlign: { xs: "center", md: "left" }
                                    }}>
                                        <Typography variant="body2"><b>Nombre:</b> {trace.name}</Typography>
                                        <Typography variant="body2"><b>Valor:</b> ${trace.value}</Typography>
                                        <Typography variant="body2"><b>Impuesto:</b> ${trace.tax}</Typography>
                                        <Typography variant="body2"><b>Fecha de venta:</b> {new Date(trace.dateSale).toLocaleDateString()}</Typography>
                                    </Box>
                                ))}
                            </Box>
                        </Box>
                    </Box>
                ) : (
                    <Typography>No hay información disponible.</Typography>
                )}
            </Box>
        </Modal>
    );
}
