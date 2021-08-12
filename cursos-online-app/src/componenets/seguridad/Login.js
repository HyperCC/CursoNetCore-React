import React, { useState } from 'react';
import style from '../Tools/Style';
import { Container, Avatar, Typography, TextField, Button } from '@material-ui/core';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import { loginUsuario } from '../../actions/UsuarioAction';

const Login = () => {

    const [usuario, setUsuario] = useState({
        Email: '',
        Password: ''
    });

    const ingresarValoresMemoria = valores => {
        const { name, value } = valores.target;
        setUsuario(anterior => ({
            ...anterior,
            [name]: value
        }));
    }

    const loginUsuarioBoton = valores => {
        valores.preventDefault();
        loginUsuario(usuario).then(response => {
            console.log('login existoso ', response);
            window.localStorage.setItem('token_seguriad', response.data.token);
        });
    }

    return (
        <Container maxWidth="xs">
            <div style={style.paper}>
                <Avatar style={style.avatar}>
                    <LockOutlinedIcon style={style.Icon} />
                </Avatar>

                <Typography component="h1" variant="h5">
                    Login de Usuario
                </Typography>

                <form style={style.form}>
                    <TextField name="Email" value={usuario.Email} onChange={ingresarValoresMemoria} variant="outlined" label="Ingrese username" fullWidth margin="normal" />
                    <TextField name="Password" value={usuario.Password} onChange={ingresarValoresMemoria} variant="outlined" type="password" label="Ingrese password" fullWidth margin="normal" />

                    <Button type="submit" onClick={loginUsuarioBoton} fullWidth variant="contained" color="primary" style={style.submit}>
                        Ingresar
                    </Button>

                </form>
            </div>
        </Container>
    );
}

export default Login;