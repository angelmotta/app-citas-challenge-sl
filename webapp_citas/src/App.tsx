import { useEffect, useState } from "react";
import "./App.css";
import RequestCitaForm from "./components/RequestCitaForm";

function App() {
    const [citas, setCitas] = useState([]);

    useEffect(() => {
        console.log(`called useEffect`);

        fetchCitas();
    }, []);

    const fetchCitas = async () => {
        console.log(`Fetching citas...`);
        const REST_API_CITAS = "http://localhost:5118/citas";
        const response = await fetch(REST_API_CITAS);
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
                {/* <RequestCitaForm fetchCitas={fetchCitas} /> */}
            </div>
            <div className="container-list-citas">
                <h2>Lista de citas</h2>
                <table>
                    <thead>
                        <tr>
                            <th>Nombre Completo</th>
                            <th>Tipo Documento</th>
                            <th>Numero Documento</th>
                            <th>Especialidad</th>
                            <th>Fecha Registrada</th>
                            <th>Hora Registrada</th>
                        </tr>
                    </thead>
                    <tbody>
                        {citas.map((cita: any) => (
                            <tr key={cita.id}>
                                <td>{cita.fullName}</td>
                                <td>{cita.docIdType}</td>
                                <td>{cita.numDocId}</td>
                                <td>{cita.specialtyName}</td>
                                <td>{cita.creationDateTime.split("T")[0]}</td>
                                <td>
                                    {
                                        cita.creationDateTime
                                            .split("T")[1]
                                            .split(".")[0]
                                    }
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </>
    );
}

export default App;
