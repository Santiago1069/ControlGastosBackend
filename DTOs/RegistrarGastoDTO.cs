namespace ControlGastosBackend.DTOs
{
    public class RegistrarGastoInputDTO
    {

        // fecha
        // fondo monetario


        //  gastos = [
        //  {TipoGastoID, Monto },
        //  {TipoGastoID, Monto} ,
        //  {TipoGastoID, Monto
        //  ]
    }

    public class RegistrarGastoOutputDTO
    {
        // tuvo presupuesto excedido: boolean
        // 
    }
}
// Servicio

// crear Registro Gasto(fecha, fondoMonetario, observaciones, )
// calcular_total_gasto = calcularTotalGasto(gastos) // suma de montos
// presupuestos_exedidos = []

// for ( gasto in gastos) 

// crear Registro Gasto Detalle(tipo gasto, monto, descripción)

// presupuesto_gasto = buscarPresupuesto de Gasto por TipoGastoId y por mes de la fecha
// presupuesto_gasto.aumentarGasto(monto) // monto ejecutado = monto ejecutado + monto

// movimiento_por_gasto = crearMovimientoPorGasto(fecha, descripcion, tipo, monto)

// if presupuesto_gasto.es_excedido() //  return boolean // monto ejecutado > monto presupuestado
    // presupuestos_excedidos.add(presupuesto_gasto)
//fin for
// fondo_montario = buscarFondoMonetarioPorId(fondoMonetarioId)
// fondo_monetario.disminuirSaldo(monto) // saldo = saldo - monto

// context.saveChanges()

// 