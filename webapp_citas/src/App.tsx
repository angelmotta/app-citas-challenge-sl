import "./App.css";
import RequestCitaForm from "./components/RequestCitaForm";

function App() {
    return (
        <>
            <div>
                <h1>Registro citas</h1>
                <RequestCitaForm />
            </div>
            <div>
                <h2>Lista de citas</h2>
                <div>Content</div>
            </div>
        </>
    );
}

export default App;
