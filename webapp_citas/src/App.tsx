import { useEffect, useState } from "react";
import "./App.css";
import RequestCitaForm from "./components/RequestCitaForm";

function App() {
    const [citas, setCitas] = useState([]);

    useEffect(() => {
        fetchCitas();
    }, []);

    const fetchCitas = async () => {
        const response = await fetch("http://localhost:3000/citas");
        // check if response is 200
        if (!response.ok) {
            // handle error
            console.error("Error fetching data");
            alert("Servicio Citas no disponible");
            return;
        }
        const data = await response.json();
        console.log(data);
        setCitas(data);
    };

    return (
        <>
            <div>
                <h1>Registro citas</h1>
                <RequestCitaForm />
            </div>
            <div>
                <h2>Lista de citas</h2>
                <table>
                    <thead>
                        <tr>
                            <th>Nombre Completo</th>
                            <th>Tipo Documento</th>
                            <th>Numero Documento</th>
                            <th>Especialidad</th>
                        </tr>
                    </thead>
                    <tbody>
                        {citas.map((cita: any) => (
                            <tr key={cita.id}>
                                <td>{cita.nombreCompleto}</td>
                                <td>{cita.tipoDocumento}</td>
                                <td>{cita.numDocumento}</td>
                                <td>{cita.especialidad}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </>
    );
}

export default App;
