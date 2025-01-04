import React, { useRef, useEffect, useState } from "react";

const Popper = ({ trigger, content, placement = 'bottom' }) => {
    const triggerRef = useRef();
    const contentRef = useRef();
    const [isOver, setIsOver] = useState(false);

    useEffect(() => {
        const triggerEl = triggerRef.current;
        const contentEl = contentRef.current;

        const handleMouseEnter = () => setIsOver(true);
        const handleMouseLeave = (e) => {
            if (!contentEl.contains(e.relatedTarget)) {
                setIsOver(false);
            }
        };

        if (triggerEl && contentEl) {
            triggerEl.addEventListener("mouseenter", handleMouseEnter);
            contentEl.addEventListener("mouseleave", handleMouseLeave);

            return () => {
                triggerEl.removeEventListener("mouseenter", handleMouseEnter);
                contentEl.removeEventListener("mouseleave", handleMouseLeave);
            };
        }
    }, []);

    return (
        <div style={{ position: 'relative' }}>
            <div ref={triggerRef}>
                {trigger}
            </div>
            <div 
                ref={contentRef}
                style={{ 
                    display: isOver ? "block" : "none",
                    position: 'absolute',
                    top: placement === 'bottom' ? '100%' : 'auto',
                    left: 0,
                    zIndex: 1000,
                    backgroundColor: 'white',
                    boxShadow: '0 2px 5px rgba(0,0,0,0.2)',
                    borderRadius: '4px',
                    padding: '10px',
                    maxWidth: '180px'
                }}
            >
                {content}
            </div>
        </div>
    );
};

export default Popper;