const API = "http://localhost:5209";
const $ = (s) => document.querySelector(s);
const show = (msg, ok=true) => { $("#mensaje").innerHTML = `<div class="alert ${ok?"alert-success":"alert-danger"}">${msg}</div>`; };
async function request(path, method="POST", body=null) {
  const opts = { method, headers: {"Content-Type":"application/json"} };
  if (body !== null) opts.body = JSON.stringify(body);
  const r = await fetch(`${API}/${path}`, opts);
  const raw = await r.text();
  if (!r.ok) throw new Error(raw || `${r.status} ${r.statusText}`);
  return raw ? JSON.parse(raw) : null;
}
function formData(form) { return Object.fromEntries(new FormData(form).entries()); }
function money(v) { return Number(v).toLocaleString("es-CO",{style:"currency",currency:"COP"}); }
async function cargar(tipo) {
  try {
    const data = await request(`${tipo}/Consultar`);
    const target = $(`#lista${tipo}`);
    target.innerHTML = data.map(x => {
      if (tipo==="Zapatos") return `<tr><td>${x.id}</td><td>${x.codigo??""}</td><td>${x.marca??""}</td><td>${x.talla}</td><td>${money(x.precio)}</td><td><button class="btn btn-sm btn-outline-danger" onclick="borrar('${tipo}',${x.id})">Eliminar</button></td></tr>`;
      if (tipo==="Clientes") return `<tr><td>${x.id}</td><td>${x.documento??""}</td><td>${x.nombre??""}</td><td>${x.telefono??""}</td><td>${x.correo??""}</td><td><button class="btn btn-sm btn-outline-danger" onclick="borrar('${tipo}',${x.id})">Eliminar</button></td></tr>`;
      return `<tr><td>${x.id}</td><td>${x.codigoZapato??""}</td><td>${x.documentoCliente??""}</td><td>${(x.fecha??"").substring(0,10)}</td><td>${x.cantidad}</td></tr>`;
    }).join("");
  } catch(e) { show(`No se pudo consultar ${tipo}: ${e.message}`,false); }
}
async function borrar(tipo,id) {
  if (!confirm("¿Deseas eliminar este registro?")) return;
  try { await request(`${tipo}/Borrar/${id}`,"DELETE"); show("Registro eliminado."); cargar(tipo); }
  catch(e) { show(e.message,false); }
}
function bind(formId,tipo,convert) {
  $(formId).addEventListener("submit",async e=>{
    e.preventDefault();
    const data=formData(e.currentTarget);
    if(convert) convert(data);
    try {
      const result=await request(`${tipo}/Insertar`,"POST",data);
      if(tipo==="Ventas") {
        $("#resultadoVenta").classList.remove("d-none");
        $("#resultadoVenta").innerHTML=`Venta registrada. Subtotal: <b>${money(result.subtotal)}</b> · Descuento: <b>${(result.porcentajeDescuento*100).toFixed(1)}%</b> (${money(result.valorDescuento)}) · Total: <b>${money(result.total)}</b>`;
      }
      show(`${tipo.slice(0,-1)} registrado correctamente.`);
      e.currentTarget.reset(); cargar(tipo);
    } catch(err) { show(err.message,false); }
  });
}
bind("#formZapato","Zapatos",d=>{d.talla=Number(d.talla);d.precio=Number(d.precio);});
bind("#formCliente","Clientes");
bind("#formVenta","Ventas",d=>{d.cantidad=Number(d.cantidad);d.fecha=d.fecha?new Date(d.fecha+"T12:00:00").toISOString():new Date().toISOString();});
(async()=>{try{await fetch(`${API}/`);$("#apiStatus").className="badge text-bg-success";$("#apiStatus").textContent="API conectada";}catch{$("#apiStatus").className="badge text-bg-danger";$("#apiStatus").textContent="API desconectada";} ["Zapatos","Clientes","Ventas"].forEach(cargar);})();
