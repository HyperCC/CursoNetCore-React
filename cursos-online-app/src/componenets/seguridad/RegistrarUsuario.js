import React, { useState } from 'react';
import { Container, Typography, Grid, TextField, Button } from '@material-ui/core';
import style from '../Tools/Style';
import { registrarUsuario } from '../../actions/UsuarioAction';


const RegistrarUsuario = () => {
    const [usuario, setUsuario] = useState({
        NombreCompleto: '',
        Email: '',
        Password: '',
        ConfirmarPassword: '',
        Username: ''
    });

    const IngresarValoresMemoria = contenidoTextfield => {
        const { name, value } = contenidoTextfield.target;
        setUsuario(anterior => ({
            ...anterior,
            [name]: value
            // NombreCompleto ..
        }));
    }

    const RegistrarUsuarioRequest = valoresActuales => {
        valoresActuales.preventDefault();

        registrarUsuario(usuario).then(response => {
            console.log('se registro exitosamente el usuario ', response);
            window.localStorage.setItem('token_seguridad', response.data.token);
        });
    }

    return (
        <Container component="main" maxWidth="md" justify="center">
            <div style={style.paper}>
                <Typography component="h1" variants="h4">
                    Registro de Usuario
                </Typography>

                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={6}>
                            <TextField name="NombreCompleto" value={usuario.NombreCompleto} onChange={IngresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su nombre y apellidos" />
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Email" value={usuario.Email} onChange={IngresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su email" />
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Username" value={usuario.Username} onChange={IngresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su username" />
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Password" value={usuario.Password} onChange={IngresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su password" />
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="ConfirmacionPassword" value={usuario.ConfirmarPassword} onChange={IngresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su confirmación para password" />
                        </Grid>
                    </Grid>

                    <Grid container justify="center">
                        <Grid item xs={12} md={6}>
                            <Button type="submit" onClick={RegistrarUsuarioRequest} fullWidth variants="contained" color="primary" size="large" style={style.submit}>
                                Enviar
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
}

export default RegistrarUsuario;