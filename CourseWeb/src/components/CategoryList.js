import React from 'react';
import { useNavigate } from 'react-router-dom';

const CategoryList = ({ categories }) => {
    const navigate = useNavigate();

    const handleCategoryClick = (searchTerm) => {
        navigate(`/category/${searchTerm}`);
    };

    return (
        <div style={styles.categoryList}>
            {categories.map((category) => (
                <div 
                    onClick={() => handleCategoryClick(category.searchTerm)}
                    style={styles.categoryItem}
                    onMouseEnter={(e) => e.target.style.color = '#8710d8'}
                    onMouseLeave={(e) => e.target.style.color = 'inherit'}
                >
                    {category.keyword}
                </div>
            ))}
        </div>
    );
};

const styles = {
    categoryList: {
        display: 'flex',
        flexDirection: 'column',
        gap: '10px',
        width: '180px',
        maxWidth: '100%'
    },
    categoryItem: {
        padding: '0px 0px 4px 8px',
        cursor: 'pointer',
        transition: 'color 0.2s',
        whiteSpace: 'nowrap',
        overflow: 'hidden',
        textOverflow: 'ellipsis'
    }
};

export default CategoryList;