import React from 'react';

const CategoryKeywords = ({ categories }) => {
    return (
        <div style={styles.categoryContainer}>
            {categories.map(category => (
                <>
                    <a href='#' className='add-basket-button' style={styles.categoryButton} key={category.searchTerm}>{category.keyword}</a>
                    <a href='#' className='add-basket-button' style={styles.categoryButton} key={category.searchTerm}>{category.keyword}</a>
                    <a href={`/category/${category.searchTerm}`} className='add-basket-button' style={styles.categoryButton} key={category.searchTerm}>{category.keyword}</a>
                </>
            ))}
        </div>
    );
}

const styles = {
    categoryButton: {
        borderRadius: '20px',
        cursor: 'pointer',
        fontWeight: 'bold',
    },
    categoryContainer: {
        display: 'flex',
        flexDirection: 'row',
        flexWrap: 'wrap',
        gap: '10px',
    }
}

export default CategoryKeywords;
