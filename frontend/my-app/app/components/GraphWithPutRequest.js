"use client";

import React, { useState, useEffect } from "react";
import { Line } from "react-chartjs-2";
import {
  Chart as ChartJS,
  LineElement,
  CategoryScale,
  LinearScale,
  PointElement,
  Tooltip,
  Legend,
} from "chart.js";

ChartJS.register(LineElement, CategoryScale, LinearScale, PointElement, Tooltip, Legend);

const GraphWithPutRequest = () => {
  const paramOptions = [
    { label: "Температура печи", value: 0 },
    { label: "Высота", value: 1 },
    { label: "Начальная температура", value: 2 },
    { label: "Коэф. теплопроводности", value: 3 },
    { label: "Теплоемкость", value: 4 },
    { label: "Плотность", value: 5 },
    { label: "Температура поверхности", value: 6 },
    { label: "Коэф. теплоотдачи", value: 7 },
  ];

  const [pathParams, setPathParams] = useState({
    index: 0,
    rangeStart: 0.1,
    rangeEnd: 4.0,
    step: 0.2,
  });

  const [bodyParams, setBodyParams] = useState({
    tPech: 1420,
    height: 0.11,
    tNach: 20,
    kTeplo: 42,
    teplo: 712,
    p: 7860,
    tPov: 1200,
    kTeploOtd: 525,
  });

  const [chartData, setChartData] = useState(null); // Данные для графика
  const [loading, setLoading] = useState(false);
  const [isHeightHalved, setIsHeightHalved] = useState(false); // Состояние чекбокса

  // Следим за изменением чекбокса
  useEffect(() => {
    setBodyParams((prev) => ({
      ...prev,
      height: isHeightHalved ? prev.height / 2 : 0.11, // Делим height на 2, если чекбокс включен
    }));
  }, [isHeightHalved]);

  const handlePathParamChange = (e) => {
    const { name, value } = e.target;
    setPathParams((prev) => ({ ...prev, [name]: value }));
  };

  const handleBodyParamChange = (e) => {
    const { name, value } = e.target;
    setBodyParams((prev) => ({ ...prev, [name]: parseFloat(value) }));
  };

  const fetchData = async () => {
    setLoading(true);

    const { index, rangeStart, rangeEnd, step } = pathParams;
    const apiUrl = `https://localhost:7057/controller/${index},${rangeStart},${rangeEnd},${step}`;

    try {
      const response = await fetch(apiUrl, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          accept: "text/plain",
        },
        body: JSON.stringify(bodyParams),
      });

      if (!response.ok) {
        throw new Error(`Ошибка: ${response.status}`);
      }

      const data = await response.json(); // Ожидаем JSON-ответ
      console.log("Ответ от API:", data);

      // Преобразуем данные для графика
      const [xValues, y1Values, y2Values] = data;

      setChartData({
        labels: xValues,
        datasets: [
          {
            label: "Время нагрева",
            data: y1Values,
            borderColor: "rgba(75,192,192,1)",
            backgroundColor: "rgba(75,192,192,0.2)",
            fill: false,
          },
          {
            label: "Температура",
            data: y2Values,
            borderColor: "rgba(153,102,255,1)",
            backgroundColor: "rgba(153,102,255,0.2)",
            fill: false,
          },
        ],
      });
    } catch (error) {
      console.error("Ошибка при запросе:", error);
    }

    setLoading(false);
  };

  return (
    <div style={{ width: "80%", margin: "50px auto" }}>
      <h1>Расчет динамических параметров</h1>

      {/* Параметры пути */}
      <div style={{ marginBottom: "20px" }}>
        <h3>Параметры диапазона</h3>
        <label>
          Параметр:
          <select
            name="index"
            value={pathParams.index}
            onChange={handlePathParamChange}
            style={{ margin: "0 10px", padding: "5px" }}
          >
            {paramOptions.map((option) => (
              <option key={option.value} value={option.value}>
                {option.label}
              </option>
            ))}
          </select>
        </label>
        <label>
          Начало диапазона:
          <input
            type="number"
            step="0.1"
            name="rangeStart"
            value={pathParams.rangeStart}
            onChange={handlePathParamChange}
            style={{ margin: "0 10px", padding: "5px" }}
          />
        </label>
        <label>
        Конец диапазона:
          <input
            type="number"
            step="0.1"
            name="rangeEnd"
            value={pathParams.rangeEnd}
            onChange={handlePathParamChange}
            style={{ margin: "0 10px", padding: "5px" }}
          />
        </label>
        <label>
          Шаг
          <input
            type="number"
            step="0.1"
            name="step"
            value={pathParams.step}
            onChange={handlePathParamChange}
            style={{ margin: "0 10px", padding: "5px" }}
          />
        </label>
      </div>

      {/* Тело запроса */}
      <div style={{ marginBottom: "20px" }}>
        <h3>Параметры печи</h3>
        {Object.keys(bodyParams).map((key) => (
          <label key={key} style={{ display: "block", margin: "5px 0" }}>
            {key}:
            <input
              type="number"
              name={key}
              value={bodyParams[key]}
              onChange={handleBodyParamChange}
              style={{ margin: "0 10px", padding: "5px" }}
            />
          </label>
        ))}

        {/* Чекбокс для изменения height */}
        <label style={{ display: "block", marginTop: "10px" }}>
          <input
            type="checkbox"
            checked={isHeightHalved}
            onChange={() => setIsHeightHalved(!isHeightHalved)}
            style={{ marginRight: "10px" }}
          />
          Нагрев с двух сторон
        </label>
      </div>

      {/* Кнопка отправки */}
      <button onClick={fetchData} disabled={loading} style={{ padding: "10px 20px", cursor: "pointer" }}>
        {loading ? "Loading..." : "Расчитать"}
      </button>

      {/* График */}
      {chartData ? (
        <div style={{ marginTop: "40px" }}>
          <Line data={chartData} />
        </div>
      ) : (
        <p style={{ marginTop: "20px" }}>Нет данных для отображения</p>
      )}
    </div>
  );
};

export default GraphWithPutRequest;
