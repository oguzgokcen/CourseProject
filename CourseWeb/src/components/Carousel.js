import React from "react";

const Carousel = () => {
    return (
        <div
            id="carouselExampleIndicators"
            className="carousel slide"
            data-bs-ride="carousel"
        >
            {/* Indicators */}
            <div className="carousel-indicators">
                <button
                    type="button"
                    data-bs-target="#carouselExampleIndicators"
                    data-bs-slide-to="0"
                    className="active"
                    aria-current="true"
                    aria-label="Slide 1"
                ></button>
                <button
                    type="button"
                    data-bs-target="#carouselExampleIndicators"
                    data-bs-slide-to="1"
                    aria-label="Slide 2"
                ></button>
                <button
                    type="button"
                    data-bs-target="#carouselExampleIndicators"
                    data-bs-slide-to="2"
                    aria-label="Slide 3"
                ></button>
            </div>

            {/* Carousel Items */}
            <div className="carousel-inner">
                <div className="carousel-item active">
                    <img
                        src="https://img-c.udemycdn.com/notices/web_carousel_slide/image/10ca89f6-811b-400e-983b-32c5cd76725a.jpg"
                        className="d-block w-100"
                        alt="Slide 1"
                    />
                    <div className="carousel-caption d-none d-md-block">
                        <h1>Slide 1</h1>
                        <p>This is the first slide.</p>
                    </div>
                </div>
                <div className="carousel-item">
                    <img
                        src="https://img-c.udemycdn.com/notices/web_carousel_slide/image/10ca89f6-811b-400e-983b-32c5cd76725a.jpg"
                        className="d-block w-100"
                        alt="Slide 2"
                    />
                    <div className="carousel-caption d-none d-md-block bg-white text-black w-25" >
                        <h1>Slide 2</h1>
                        <p>This is the second slide.</p>
                    </div>
                </div>
                <div className="carousel-item">
                    <img
                        src="https://img-c.udemycdn.com/notices/web_carousel_slide/image/10ca89f6-811b-400e-983b-32c5cd76725a.jpg"
                        className="d-block w-100"
                        alt="Slide 3"
                    />
                    <div className="carousel-caption d-none d-md-block bg-white text-black w-25">
                        <h5>Slide 3</h5>
                        <p>This is the third slide.</p>
                    </div>
                </div>
            </div>

            {/* Navigation Controls */}
            <button
                className="carousel-control-prev"
                type="button"
                data-bs-target="#carouselExampleIndicators"
                data-bs-slide="prev"
            >
                <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                <span className="visually-hidden">Previous</span>
            </button>
            <button
                className="carousel-control-next"
                type="button"
                data-bs-target="#carouselExampleIndicators"
                data-bs-slide="next"
            >
                <span className="carousel-control-next-icon" aria-hidden="true"></span>
                <span className="visually-hidden">Next</span>
            </button>
        </div>
    );
};

export default Carousel;
