import React, { useState } from "react";
import { Line } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

const GraphWithInputs = () => {
  // Состояние для ввода данных
  const [index, setIndex] = useState(0);
  const [rangeStart, setRangeStart] = useState(0.1);
  const [rangeEnd, setRangeEnd] = useState(3.9);
  const [step, setStep] = useState(0.2);

  const [chartData, setChartData] = useState(null);
  const [loading, setLoading] = useState(false);

  const fetchData = async () => {
    setLoading(true);

    try {
      const response = await fetch("https://localhost:7057/controller/", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          index: parseInt(index, 10), // Преобразуем к числу
          rangeStart: parseFloat(rangeStart),
          rangeEnd: parseFloat(rangeEnd),
          step: parseFloat(step),
        }),
      });

      if (!response.ok) {
        throw new Error("Ошибка при получении данных");
      }

      const apiData = await response.json();

      // Предполагаем, что API возвращает массив с тремя наборами данных
      const xValues = apiData[0];
      const yValues1 = apiData[1];
      const yValues2 = apiData[2];

      setChartData({
        labels: xValues,
        datasets: [
          {
            label: "Dataset 1 (Y1)",
            data: yValues1,
            borderColor: "rgba(75,192,192,1)",
            backgroundColor: "rgba(75,192,192,0.2)",
            tension: 0.4,
          },
          {
            label: "Dataset 2 (Y2)",
            data: yValues2,
            borderColor: "rgba(255,99,132,1)",
            backgroundColor: "rgba(255,99,132,0.2)",
            tension: 0.4,
          },
        ],
      });
    } catch (error) {
      console.error("Ошибка загрузки данных:", error);
    }

    setLoading(false);
  };

  return (
    <div style={{ width: "80%", margin: "50px auto" }}>
      <h1>Graph Example with User Input</h1>

      {/* Ввод данных */}
      <div style={{ marginBottom: "20px" }}>
        <label>
          Index (int):
          <input
            type="number"
            value={index}
            onChange={(e) => setIndex(e.target.value)}
            style={{ margin: "0 10px", padding: "5px" }}
          />
        </label>
        <label>
          Range Start (float):
          <input
            type="number"
            step="0.1"
            value={rangeStart}
            onChange={(e) => setRangeStart(e.target.value)}
            style={{ margin: "0 10px", padding: "5px" }}
          />
        </label>
        <label>
          Range End (float):
          <input
            type="number"
            step="0.1"
            value={rangeEnd}
            onChange={(e) => setRangeEnd(e.target.value)}
            style={{ margin: "0 10px", padding: "5px" }}
          />
        </label>
        <label>
          Step (float):
          <input
            type="number"
            step="0.1"
            value={step}
            onChange={(e) => setStep(e.target.value)}
            style={{ margin: "0 10px", padding: "5px" }}
          />
        </label>
      </div>

      <button onClick={fetchData} disabled={loading} style={{ padding: "10px 20px", cursor: "pointer" }}>
        {loading ? "Loading..." : "Generate Graph"}
      </button>

      {/* График */}
      {chartData && <Line data={chartData} />}
    </div>
  );
};

export default GraphWithInputs;
